using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers.v1;
using WebApi.Entities;
using WebApi.Interface.V1;
using WebApi.Models.V1;
using WebApi.Service;

namespace WebApiUnitTesting.V1.Controller.StudentControllerTest
{
    public class GetAllStudentTest
    {
        private readonly Mock<IStudentRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StudentController _controller;

        public GetAllStudentTest()
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
            Assert.Equal("Page number and page size must be greater than 0.", badRequestResult.Value);
        }
        [Fact]
        public async Task GetStudentsAsync_ReturnsPagedStudents_WhenValidInput()
        {
            int pageNumber = 1;
            int pageSize = 2;
            var students = new List<Student>
                        {
                            new Student { 
                                    Id = 1,
                                    Name = "John Doe",
                                    Password = "123",
                                    Address = "123 Main St",
                                    Email = "test@gmail.com",
                                    Phone = "1234567890",
                                    Age = 20,
                                    DateOfBirth = DateTime.Now.AddYears(-18)
                            },
                            new Student { Id = 2,
                                Name = "John Doe",
                                Password = "123",
                                Address = "123 Main St",
                                Email = "test@gmail.com",
                                Phone = "1234567890",
                                Age = 20,
                                DateOfBirth = DateTime.Now.AddYears(-18)
                            }
                        };

            var pagedStudents = new PagedResult<Student>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = 2,
                Data = students
            };

            var studentDtos = new List<StudentReadDto>
                        {
                            new StudentReadDto { 
                                    Id = 1,
                                    Name = "John Doe",
                                    Address = "123 Main St",
                                    Email = "test@gmail.com",
                                    Phone = "1234567890",
                                    Age = 20,
                                    DateOfBirth = DateTime.Now
                            },
                            new StudentReadDto { 
                                Id = 2,
                                Name = "John Doe",
                                Address = "123 Main St",
                                Email = "test@gmail.com",
                                Phone = "1234567890",
                                Age = 20,
                                DateOfBirth = DateTime.Now
                            }
                        };

            _mockRepo.Setup(r => r.GetStudentsAsync(pageNumber, pageSize))
                     .ReturnsAsync(pagedStudents);

            _mockMapper.Setup(m => m.Map<List<StudentReadDto>>(students))
                       .Returns(studentDtos);

            // Act
            var result = await _controller.GetStudentsAsync(pageNumber, pageSize);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PagedResult<StudentReadDto>>(okResult.Value);

            Assert.Equal(2, returnValue.TotalCount);
            Assert.Equal(2, returnValue.Data.Count);
            Assert.Equal("John Doe", returnValue.Data[0].Name);


        }
    }
}
