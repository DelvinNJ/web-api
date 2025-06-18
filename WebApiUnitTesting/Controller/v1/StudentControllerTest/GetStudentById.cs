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

namespace WebApiUnitTesting.Controller.V1.StudentControllerTest
{
    public class GetStudentById
    {
        private readonly Mock<IStudentRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StudentController _controller;
        public GetStudentById()
        {
            _mockRepo = new Mock<IStudentRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new StudentController(_mockMapper.Object, _mockRepo.Object);
        }

        [Fact]
        public async Task GetStudentById_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            int studentId = 1;
            _mockRepo.Setup(repo => repo.GetStudentByIdAsync(studentId)).ReturnsAsync((Student?)null);
            // Act
            var result = await _controller.GetStudentByIdAsync(studentId);
            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal($"Student with ID {studentId} not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetStudentById_ReturnsStudent_WhenStudentExists()
        {
            // Arrange
            int studentId = 1;
            var student = new Student
            {
                Id = studentId,
                Name = "John Doe",
                Password = "123",
                Address = "123 Main St",
                Email = "test@gmail.com",
                Phone = "1234567890",
                Age = 20,
                DateOfBirth = DateTime.Now
            };
            var studentDto = new StudentReadDto
            {
                Id = studentId,
                Name = "John Doe",
                Address = "123 Main St",
                Email = "test@gmail.com",
                Phone = "1234567890",
                Age = 20,
                DateOfBirth = DateTime.Now
            };

            _mockRepo.Setup(repo => repo.GetStudentByIdAsync(studentId)).ReturnsAsync(student);
            _mockMapper.Setup(m => m.Map<StudentReadDto>(student)).Returns(studentDto);
            
            // Act
            var result = await _controller.GetStudentByIdAsync(studentId);
            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(studentDto, okResult.Value);
        }
    }
}
