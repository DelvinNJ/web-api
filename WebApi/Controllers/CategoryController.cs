using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entity;
using WebApi.Interface;
using WebApi.Models;
using WebApi.Repository;
using WebApi.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryInterface;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryInterface, IMapper mapper)
        {
            _categoryInterface = categoryInterface;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] int page = 1,
                                                        [FromQuery] int pageSize = 10,
                                                        [FromQuery] string sortBy = "Id",
                                                        [FromQuery] string sortOrder = "desc",
                                                        [FromQuery] string? include = null,
                                                        [FromQuery] string? search = null)
        {
            var result = await _categoryInterface.GetCategories(page, pageSize, sortBy, sortOrder, include, search);

            var response = new PagedResult<CategoryDto>
            {
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                Items = _mapper.Map<List<CategoryDto>>(result.Items)
            };

            return Ok(response);
        }
    }
}
