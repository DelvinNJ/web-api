using WebApi.Entity;
using WebApi.Service;

namespace WebApi.Interface
{
    public interface ICategoryRepository
    {
        Task<PagedResult<Category>> GetCategories(int page,
                                                  int pageSize,
                                                  string sortBy,
                                                  string sortOrder,
                                                  string? include = null,
                                                  string? search = null);
    }
}
