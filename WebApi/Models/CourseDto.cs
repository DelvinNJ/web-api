using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApi.Entity;

namespace WebApi.Models
{
    public class CourseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public decimal? Discount { get; set; }

    }
}
