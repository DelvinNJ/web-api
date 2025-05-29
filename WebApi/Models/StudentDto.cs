using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApi.Entity;

namespace WebApi.Models
{
    public class StudentDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public List<CourseDto>? Courses { get; set; }
    }
}
