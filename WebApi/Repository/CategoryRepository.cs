using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entity;
using WebApi.Interface;
using WebApi.Service;

namespace WebApi.Repository
{
    public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext = context;

        public async Task<PagedResult<Category>> GetCategories(int page,
                                                       int pageSize,
                                                       string sortBy,
                                                       string sortOrder,
                                                       string? include = null,
                                                       string? search = null)
        {
            IQueryable<Category> query = _dbContext.Categories;

            var allowedIncludes = new[] { "Products" };
            query = query.ApplyIncludes(allowedIncludes, include);

            if (!string.IsNullOrWhiteSpace(search))
            {
                string[] fields = { "Name", "Description" };
                query = query.ApplySearch(search, fields);
            }

            query = query.ApplySorting(sortBy, sortOrder);

            return await query.ToPagedResultAsync(page, pageSize);
        }
    }
}
