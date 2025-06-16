using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entity;
using WebApi.Interface;
using WebApi.Models;
using WebApi.Service;

namespace WebApi.Repository
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
            IQueryable<Student> query = _dbContext.Students;

            return await query.ToPagedResultAsync(pageNumber, pageSize);
        }

       public async Task<Student?> GetStudentByIdAsync(int id)
       {
            return await _dbContext.Students.FindAsync(id);
       }

        public async Task<Student?> CreateStudentAsync(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }
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