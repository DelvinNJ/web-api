using WebApi.Entity;

namespace WebApi.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public List<IncludeProductDto>? Products { get; set; }
    }   
}
