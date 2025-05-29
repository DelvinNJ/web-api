using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using WebApi.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;
using WebApi.Interface;
using AutoMapper;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDonorRepository _donorRepository;
        private readonly IMapper _mapper;

        public DonorController(ApplicationDbContext dbContext, IDonorRepository donorRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _donorRepository = donorRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDonor()
        {
            var donors = await _donorRepository.GetDonors();
            return Ok(_mapper.Map<List<DonorDto>>(donors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDonor(DonorDto donorDto)
        {
            var createDonor = await _donorRepository.CreateDonor(donorDto);
            return Ok(_mapper.Map<DonorDto>(createDonor));
        }

        [HttpGet("{id}", Name = "get-vendor")]
        public async Task<IActionResult> GetDonor(int id)
        {
            var donor = await _donorRepository.GetDonor(id);
            if (donor == null) {
                return BadRequest();
            }
            return Ok(donor);
        }

        [HttpPut("{id}")]
        public IActionResult updateDonor(int id, DonorDto addDonorDto)
        {
            var donor = _dbContext.Donors.Find(id);
            if (donor is null)
            {
                return NotFound();
            }
            donor.Name = addDonorDto.Name;
            donor.DateOfBirth = addDonorDto.DateOfBirth;
            donor.PhoneNumber = addDonorDto.PhoneNumber;
            _dbContext.SaveChanges();
            return Ok(donor);
        }

        [HttpDelete("{id}")]
        public IActionResult deleteDonor(int id)
        {
            var donor = _dbContext.Donors.Find(id);
            if(donor is null)
            {
                return NotFound();
            }
            _dbContext.Donors.Remove(donor);
            _dbContext.SaveChanges();
            return Ok();
        }

    }
}
