using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApi.Entities;
using WebApi.Service;

namespace WebApi.Interface.V1
{
    public interface IStudentRepository
    {
       Task<PagedResult<Student>> GetStudentsAsync(
           int pageNumber, 
           int pageSize);
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student?> DeleteStudentByIdAsync(int id);
        Task<Student?> CreateStudentAsync(Student student);
        Task<Student> UpdateStudentAsync(Student student);

    }
}
