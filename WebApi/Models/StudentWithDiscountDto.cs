namespace WebApi.Models
{
    public class StudentWithDiscountDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Discount { get; set; }
    }
}
