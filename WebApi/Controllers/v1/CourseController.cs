using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApi.Entity;
using WebApi.Interface.V1;
using WebApi.Models.V1;
using WebApi.Service;

namespace WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/course/")]
    [ApiVersion("1.0")]
    public class CourseController(ICourseRepository courseRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly IMapper _mapper = mapper;


        [HttpGet]
        public async Task<ActionResult<PagedResult<CourseReadDto?>>> GetAllCourseAsync(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }
            var pagedResult = await _courseRepository.GetAllCoursesAsync(pageNumber, pageSize);


            return Ok(
                new PagedResult<CourseReadDto?>
                {
                    PageNumber = pagedResult.PageNumber,
                    PageSize = pagedResult.PageSize,
                    TotalCount = pagedResult.TotalCount,
                    Version = "Version 1.0",
                    Data = _mapper.Map<List<CourseReadDto?>>(pagedResult.Data)
                }
            );
        }
        [HttpPost]
        public async Task<ActionResult<CourseReadDto?>> CreateCourseAsync([FromBody] CourseCreateDto courseDto)
        {
            var data = _mapper.Map<Course>(courseDto);
            var course = await _courseRepository.AddCourseAsync(data);
            return Ok(_mapper.Map<CourseReadDto>(course));
        }
    }

}
