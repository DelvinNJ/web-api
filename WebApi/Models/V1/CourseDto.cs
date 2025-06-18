using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.V1
{
    public class CourseCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
       
    }
    public class CourseReadDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public List<CourseStudentDto>? Students { get; set; }
    }

    public class  CourseStudentDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Discount { get; set; }
    }
}
