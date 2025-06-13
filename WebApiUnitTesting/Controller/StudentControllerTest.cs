using AutoMapper;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Interface;

namespace WebApiUnitTesting.Controller
{
    public class StudentControllerTest
    {
        private IStudentRepository _studentRepository;
        private IMapper _mapper;
        private IStudentRepository _istudentRespository;

        public StudentControllerTest()
        {
            _studentRepository = A.Fake<IStudentRepository>();
            _mapper = A.Fake<IMapper>();
            _istudentRespository = A.Fake<IStudentRepository>();
        }


    }
}
