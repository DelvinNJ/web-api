using System.Threading.Tasks;
using WebApi.Entity;
using WebApi.Models;
using WebApi.Service;

namespace WebApi.Interface
{
    public interface IProductRepository
    {
        Task<PagedResult<Product>> GetProducts(int page, int pageSize, string sortBy, string sortOrder, string? include = null);
    }
}
