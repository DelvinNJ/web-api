using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entity;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Repository
{
    public class DonorRepository : IDonorRepository
    {
        private readonly ApplicationDbContext _context;

        public DonorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Donor>> GetDonors()
        {
            return await _context.Donors.ToListAsync();
        }
        public async Task<Donor> CreateDonor(DonorDto donorDto)
        {
            var donorEntity = new Donor
            {
                Name = donorDto.Name,
                PhoneNumber = donorDto.PhoneNumber,
                DateOfBirth = donorDto.DateOfBirth,
            };
            await _context.Donors.AddAsync(donorEntity);
            await _context.SaveChangesAsync();
            return donorEntity;
        }
        public async Task<Donor?> GetDonor(int id)
        {
            var donor =  await _context.Donors.FindAsync(id);
            return donor;
        }
    }
}
