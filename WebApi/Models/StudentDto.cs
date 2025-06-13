using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class StudentDto
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? address { get; set; }
    }
}
