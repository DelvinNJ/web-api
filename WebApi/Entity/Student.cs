using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApi.Models;

namespace WebApi.Entity
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = null!;
    }
}
