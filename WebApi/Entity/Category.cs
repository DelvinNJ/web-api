using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace WebApi.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
