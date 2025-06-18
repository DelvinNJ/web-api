using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApi.DbContexts;
using WebApi.Entities;
using WebApi.Interface.V1;
using WebApi.Service;

namespace WebApi.Repository.V1
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResult<Student>> GetStudentsAsync(
            int pageNumber,
            int pageSize)
        {
            IQueryable<Student> query = _dbContext.Students
                    .Include(s => s.StudentCourses)
                     .ThenInclude(sc => sc.Course);

            return await query.ToPagedResultAsync(pageNumber, pageSize);
        }

       public async Task<Student?> GetStudentByIdAsync(int id)
       {
            return await _dbContext.Students.FindAsync(id);
       }

        public async Task<Student?> CreateStudentAsync(Student student)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
            return student;
        }
        public async Task<Student> UpdateStudentAsync(Student student)
        {
            _dbContext.Students.Update(student); 
            await _dbContext.SaveChangesAsync();
            return student;
        }
        public async Task<Student?> DeleteStudentByIdAsync(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            if (student == null)
            {
                return null;
            }
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return student;
        }
    }

}