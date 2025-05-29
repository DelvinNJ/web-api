using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }
        public string? ProductType { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
