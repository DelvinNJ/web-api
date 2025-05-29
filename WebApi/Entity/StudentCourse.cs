namespace WebApi.Entity
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public required Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public decimal Discount { get; set; } = 0;
    }
}
