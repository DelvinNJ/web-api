using System.ComponentModel.DataAnnotations;
using WebApi.Helper;

namespace WebApi.Models.V1
{
    public class StudentCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = String.Empty;
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        public string Address { get; set; } = String.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = String.Empty;
        [Required]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; } = String.Empty;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = String.Empty;
        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; } = String.Empty;
        [Required]
        [MinimumAge(18)]
        public DateTime DateOfBirth { get; set; }
    }
    public class StudentReadDto {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }

        public required string Email { get; set; }

        public required string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<StudentCourseDto>? Courses { get; set; }

    }
    public class StudentCourseDto
    {
       public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public decimal Discount { get; set; }
    }
}
