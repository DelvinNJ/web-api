using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Entity
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
        public string? address { get; set; }
    }
}
