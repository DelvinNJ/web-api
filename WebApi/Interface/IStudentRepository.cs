using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApi.Entity;
using WebApi.Service;

namespace WebApi.Interface
{
    public interface IStudentRepository
    {
       Task<PagedResult<Student>> GetStudentsAsync(
           int pageNumber, 
           int pageSize, 
           string? include = null,
           string? filter = null, 
           string? search = null);

    }
}
