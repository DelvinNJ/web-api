namespace WebApi.Models
{
    public class DonorDto
    {
        public required string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
