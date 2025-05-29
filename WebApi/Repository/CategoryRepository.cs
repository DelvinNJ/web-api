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
                                                               string? include = null)
        {

            IQueryable<Category> query = _dbContext.Categories;

            string[] allowedIncludes = {"Products", "Products.Variants"};
            query = query.ApplyIncludes(include, allowedIncludes);

            return await query
                .GetQueryAsync(page, pageSize, sortBy, sortOrder);
        }
    }
}
