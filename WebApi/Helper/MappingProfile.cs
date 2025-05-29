using AutoMapper;
using WebApi.Entity;
using WebApi.Models;

namespace WebApi.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, IncludeProductDto>();


            CreateMap<Product, ProductDto>();
            CreateMap<Category, IncludeCategoryDto>();

            CreateMap<Donor, DonorDto>();


            CreateMap<Student, StudentDto>()
             .ForMember(dest => dest.Courses,
                 opt => opt.MapFrom(src => src.StudentCourses));

            CreateMap<StudentCourse, CourseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Course.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount));

            CreateMap<Course, CourseWithStudentsDto>()
            .ForMember(dest => dest.Students,
                opt => opt.MapFrom(src => src.StudentCourses));

            CreateMap<StudentCourse, StudentWithDiscountDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Student.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount));
        }
    }
}
