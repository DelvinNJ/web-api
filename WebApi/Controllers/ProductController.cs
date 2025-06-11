using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entity;
using WebApi.Interface;
using WebApi.Models;
using WebApi.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        //[HttpGet]
        //public async Task<IActionResult> GetProducts([FromQuery] int page = 1,
        //                                             [FromQuery] int pageSize = 10,
        //                                             [FromQuery] string sortBy = "Name",
        //                                             [FromQuery] string sortOrder = "desc",
        //                                             [FromQuery] string? include = null)
        //{
        //    string?[] allowedIncludes = new[] { "Category" };

        //    var result = await _productRepository.GetProducts(page,
        //                                                      pageSize,
        //                                                      sortBy,
        //                                                      sortOrder,
        //                                                      allowedIncludes,
        //                                                      include);

        //    var pagedResult = new PagedResult<ProductDto>
        //    {
        //        PageNumber = result.PageNumber,
        //        PageSize = result.PageSize,
        //        TotalCount = result.TotalCount,
        //        Items = _mapper.Map<List<ProductDto>>(result.Items)
        //    };

        //    return Ok(pagedResult);
        //}
    }
}
