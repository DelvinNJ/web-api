namespace WebApi.Entity
{
    public class Donor
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
