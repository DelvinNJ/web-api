using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers.v1;
using WebApi.Entities;
using WebApi.Interface.V1;

namespace WebApiUnitTesting.V1.Controller.StudentControllerTest
{
    public class DeleteStudentById
    {
        private readonly Mock<IStudentRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StudentController _controller;
        public DeleteStudentById()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepo = new Mock<IStudentRepository>();
            _controller = new StudentController(_mockMapper.Object, _mockRepo.Object);
        }

        [Fact]
        public async Task DeleteStudentByIdAsync_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            int studentId = 1;
            _mockRepo.Setup(repo => repo.DeleteStudentByIdAsync(studentId)).ReturnsAsync((Student?)null);
            // Act
            var result = await _controller.DeleteStudentByIdAsync(studentId);
            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Student with ID {studentId} not found.", notFoundResult.Value);
        }
        [Fact]
        public async Task DeleteStudentByIdAsync_ReturnsOk_WhenStudentIsDeleted()
        {
            
            int studentId = 1;
            var student = new Student { 
                Id = studentId, 
                Name = "John Doe", 
                Password = "123",
                Address = "123 Main St",
                Email = "test@gmail.com",
                Phone = "1234567890",
                Age = 20,
                DateOfBirth = DateTime.Now
            };

            _mockRepo.Setup(repo => repo.GetStudentByIdAsync(studentId))
                     .ReturnsAsync(student);

            _mockRepo.Setup(repo => repo.DeleteStudentByIdAsync(studentId))
                 .ReturnsAsync(student);

           // Act
           var result = await _controller.DeleteStudentByIdAsync(studentId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
