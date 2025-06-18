using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApi.DbContexts;
using WebApi.Entities;
using WebApi.Entity;
using WebApi.Interface.V1;
using WebApi.Service;

namespace WebApi.Repository.V1
{
    public class CourseRepository(ApplicationDbContext dbContext) : ICourseRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<PagedResult<Course>> GetAllCoursesAsync(int pageNumber, int pageSize)
        {
            IQueryable<Course> query = _dbContext.Courses
                                         .Include(s => s.StudentCourses)
                                        .ThenInclude(sc => sc.Student);

            return await query.ToPagedResultAsync(pageNumber, pageSize);
        }
        public async Task<Course> AddCourseAsync(Course course)
        {
            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();
            return course;
        }
    }
}
