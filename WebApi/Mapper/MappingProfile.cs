using AutoMapper;
using WebApi.Entities;
using WebApi.Entity;
using WebApi.Models.V1;

namespace WebApi.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //Student
            CreateMap<StudentCreateDto, Student>();

            CreateMap<Student, StudentReadDto>()
                .ForMember(dest => dest.Courses,
                opt => opt.MapFrom(src => src.StudentCourses));

            CreateMap<StudentCourse, StudentCourseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Course.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Course.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Course.Price))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount));


            //Course
            CreateMap<CourseCreateDto, Course>();

            CreateMap<Course, CourseReadDto>()
                .ForMember(dest => dest.Students,
                opt => opt.MapFrom(src => src.StudentCourses));
            CreateMap<StudentCourse, CourseStudentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Student.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src=> src.Discount));

        }
    }
}
