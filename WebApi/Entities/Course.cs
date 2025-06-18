using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Entity
{
    public class Course
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = null!;
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;

        public ICollection<StudentCourse> StudentCourses { get; set; } = null!;
    }
}
