using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entity;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var students = _context.Students
            .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
            .ToList();

            var studentDtos = _mapper.Map<List<StudentDto>>(students);
            return Ok(studentDtos);
        }

        [HttpGet("courses-with-students")]
        public async Task<ActionResult<IEnumerable<CourseWithStudentsDto>>> GetCoursesWithStudents()
        {
            var courses = await _context.Courses
                .Include(c => c.StudentCourses)
                    .ThenInclude(sc => sc.Student)
                .ToListAsync();

            var courseDtos = _mapper.Map<List<CourseWithStudentsDto>>(courses);
            return Ok(courseDtos);
        }
    }
}
