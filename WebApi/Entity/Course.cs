using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Entity
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public ICollection<StudentCourse> StudentCourses { get; set; } = null!;
    }
}
