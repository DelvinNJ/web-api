using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entity;
using WebApi.Interface;
using WebApi.Mapper;
using WebApi.Models;
using WebApi.Repository;
using WebApi.Service;

namespace WebApi.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("Api/V1/[controller]/")]
    public class StudentController: ControllerBase {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public StudentController(IMapper mapper, IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }
        [HttpGet]
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDto>> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if(student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            else
            {
                return Ok(_mapper.Map<StudentDto>(student));
            }
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudentAsync([FromBody] StudentDto studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Student data is required.");
            }
            var data = _mapper.Map<Student>(studentDto);
            var student = await _studentRepository.CreateStudentAsync(data);
            return Ok(_mapper.Map<StudentDto>(student));

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentDto>> UpdateStudentAsync(int id, [FromBody] StudentDto studentDto)
        {
            
            var existingStudent = await _studentRepository.GetStudentByIdAsync(id);
            if (existingStudent == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            _mapper.Map(studentDto, existingStudent);

            var updateStudent = await _studentRepository.UpdateStudentAsync(existingStudent);

            return Ok(_mapper.Map<StudentDto>(updateStudent));
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteStudentAsync(int id)
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
