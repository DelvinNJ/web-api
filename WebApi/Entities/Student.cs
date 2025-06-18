using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApi.Entity;

namespace WebApi.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
        [Range(0, 120)]
        public int Age { get; set; }
        public string? Address { get; set; }
        [Required]
        [MinLength(4)]
        public required string Password { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Phone { get; set; }
        [Required]
        public required DateTime DateOfBirth { get; set; } = DateTime.Now;

        public ICollection<StudentCourse>? StudentCourses { get; set; }
    }
}
