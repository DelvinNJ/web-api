using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entity;
using WebApi.Interface;
using WebApi.Models;
using WebApi.Service;

namespace WebApi.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task<PagedResult<Product>> GetProducts(int page,
                                                            int pageSize,
                                                            string sortBy,
                                                            string sortOrder,
                                                            string? include = null)
        {
            IQueryable<Product> query = _dbContext.Products;

            string[] allowedIncludes = { "Category", "Variants" };
            query = query.ApplyIncludes(include, allowedIncludes);

            return await query.GetQueryAsync(page, pageSize, sortBy, sortOrder);
        }
    }
}
