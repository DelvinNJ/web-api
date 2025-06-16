using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity;
using WebApi.Interface;
using WebApi.Models;
using WebApi.Service;
using WebApi.V1.Controllers;

namespace WebApiUnitTesting.V1.Controller.StudentControllerTest
{
    public class GetStudentTest
    {
        private readonly Mock<IStudentRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StudentController _controller;

        public GetStudentTest()
        {
            _mockRepo = new Mock<IStudentRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new StudentController(_mockMapper.Object, _mockRepo.Object);
        }

        [Fact]
        public async Task GetStudentsAsync_ReturnsBadRequest_WhenPageNumberOrPageSizeIsInvalid()
        {

            int invalidPageNumber = 0;
            int invalidPageSize = 0;
            // Act
            var result = await _controller.GetStudentsAsync(invalidPageNumber, invalidPageSize);
            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Page number must be greater than 0.", badRequestResult.Value);
        }
        [Fact]
        public async Task GetStudentsAsync_ReturnsPagedStudents_WhenValidInput()
        {
            int pageNumber = 1;
            int pageSize = 2;
            var students = new List<Student>
                        {
                            new Student { Id = 1, Name = "Alice" },
                            new Student { Id = 2, Name = "Bob" }
                        };

            var pagedStudents = new PagedResult<Student>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = 2,
                Data = students
            };

            var studentDtos = new List<StudentDto>
                        {
                            new StudentDto { Id = 1, Name = "Alice" },
                            new StudentDto { Id = 2, Name = "Bob" }
                        };

            _mockRepo.Setup(r => r.GetStudentsAsync(pageNumber, pageSize))
                     .ReturnsAsync(pagedStudents);

            _mockMapper.Setup(m => m.Map<List<StudentDto>>(students))
                       .Returns(studentDtos);

            // Act
            var result = await _controller.GetStudentsAsync(pageNumber, pageSize);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PagedResult<StudentDto>>(okResult.Value);

            Assert.Equal(2, returnValue.TotalCount);
            Assert.Equal(2, returnValue.Data.Count);
            Assert.Equal("Alice", returnValue.Data[0].Name);


        }
    }
}
