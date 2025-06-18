using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApi.Entity;
using WebApi.Service;

namespace WebApi.Interface.V1
{
    public interface ICourseRepository
    {
        Task<PagedResult<Course>> GetAllCoursesAsync(int pageNumber, int pageSize);
        Task<Course> AddCourseAsync(Course course);
    }
}
