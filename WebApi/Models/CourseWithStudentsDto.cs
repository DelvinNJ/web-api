namespace WebApi.Models
{
    public class CourseWithStudentsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public List<StudentWithDiscountDto>? Students { get; set; }
    }
}
