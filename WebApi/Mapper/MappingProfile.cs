using AutoMapper;
using WebApi.Entity;
using WebApi.Models;

namespace WebApi.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
        }
    }
}
