using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Helper;

namespace WebApiUnitTesting.Helper
{
    public class MinimumAgeAttributeTests
    {
        [Theory]
        [InlineData("2000-01-01", 18, true)]  // Age > 18, valid
        [InlineData("2025-01-01", 18, false)] // Age < 18, invalid
        [InlineData(null, 18, true)]          // Null value, valid (depends on your logic)
        public void IsValid_ReturnsExpectedResult(string dateString, int minAge, bool expectedIsValid)
        {
            // Arrange
            var attribute = new MinimumAgeAttribute(minAge);

            object? value = dateString == null ? null : DateTime.Parse(dateString); 
            var validationContext = new ValidationContext(new object());

            // Act
            var result = attribute.GetValidationResult(value, validationContext);

            // Assert
            if (expectedIsValid)
            {
                Assert.Equal(ValidationResult.Success, result);
            }
            else
            {
                Assert.NotEqual(ValidationResult.Success, result);
                Assert.Contains($"Age must be at least {minAge}", result?.ErrorMessage);
            }
        }

        [Fact]
        public void IsValid_ReturnsError_ForInvalidType()
        {
            // Arrange
            var attribute = new MinimumAgeAttribute(18);
            var validationContext = new ValidationContext(new object());

            // Passing an invalid type (e.g. string)
            var result = attribute.GetValidationResult("invalid type", validationContext);

            // Assert
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.Equal("Invalid input type. Date of birth is required.", result?.ErrorMessage);
        }
    }
}
