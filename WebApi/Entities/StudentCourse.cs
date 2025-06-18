using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Entities;

namespace WebApi.Entity
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public required Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; } = 0;
    }
}
