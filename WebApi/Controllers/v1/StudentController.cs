using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApi.DbContexts;
using WebApi.Entities;
using WebApi.Interface.V1;
using WebApi.Mapper;
using WebApi.Models.V1;
using WebApi.Service;

namespace WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/student/")]
    [ApiVersion("1.0")]
    public class StudentController(IMapper mapper, IStudentRepository studentRepository) : ControllerBase {
        private readonly IMapper _mapper = mapper;
        private readonly IStudentRepository _studentRepository = studentRepository;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<PagedResult<StudentReadDto>>> GetStudentsAsync(int pageNumber = 1, int pageSize = 10)
        {
            if(pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var pagedResult = await _studentRepository.GetStudentsAsync(pageNumber, pageSize);
            return Ok(new PagedResult<StudentReadDto>
            {
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize,
                TotalCount = pagedResult.TotalCount,
                Version = "Version 1.0",
                Data = _mapper.Map<List<StudentReadDto>>(pagedResult.Data)
            });
        }

        [HttpPost]
        public async Task<ActionResult<StudentReadDto>> CreateStudentAsync([FromBody] StudentCreateDto studentDto)
        {
            var data = _mapper.Map<Student>(studentDto);
            var student = await _studentRepository.CreateStudentAsync(data);
            return Ok(_mapper.Map<StudentReadDto>(student));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentReadDto>> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            else
            {
                return Ok(_mapper.Map<StudentReadDto>(student));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentReadDto>> UpdateStudentAsync(int id, [FromBody] StudentCreateDto studentDto)
        {
            
            var existingStudent = await _studentRepository.GetStudentByIdAsync(id);
            if (existingStudent == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            _mapper.Map(studentDto, existingStudent);

            var updateStudent = await _studentRepository.UpdateStudentAsync(existingStudent);

            return Ok(_mapper.Map<StudentReadDto>(updateStudent));
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            await _studentRepository.DeleteStudentByIdAsync(id);
            return NoContent();
        }

     }
}
