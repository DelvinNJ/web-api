using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class StudentDto
    {
        public int? Id { get; set;  }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? address { get; set; }
    }
}
