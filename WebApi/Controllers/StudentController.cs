using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entity;
using WebApi.Interface;
using WebApi.Mapper;
using WebApi.Models;
using WebApi.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class StudentController: ControllerBase {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public StudentController(IMapper mapper, IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }
        [HttpGet("api/students")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<PagedResult<StudentDto>>> GetStudentsAsync(int pageNumber = 1, int pageSize = 10)
        {
            if(pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number must be greater than 0.");
            }

            var pagedResult = await _studentRepository.GetStudentsAsync(pageNumber, pageSize);
            return Ok(new PagedResult<StudentDto>
            {
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize,
                TotalCount = pagedResult.TotalCount,
                Data = _mapper.Map<List<StudentDto>>(pagedResult.Data)
            });
        }
    }
}
