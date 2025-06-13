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
            int pageSize, 
            string? include = null,
            string? filter = null, 
            string? search = null)
        {
            IQueryable<Student> query = _dbContext.Students;

            return await query.ToPagedResultAsync(pageNumber, pageSize);
        }
    }

}