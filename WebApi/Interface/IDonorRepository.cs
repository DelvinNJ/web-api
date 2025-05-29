using WebApi.Entity;
using WebApi.Models;

namespace WebApi.Interface
{
    public interface IDonorRepository
    {
        Task<ICollection<Donor>> GetDonors();
        Task<Donor> CreateDonor(DonorDto donorDto);
        Task<Donor?> GetDonor(int id);
    }
}
