using AutoMapper;
using STCS.Infrastructure.Entities;
using CourseBO = STCS.Infrastructure.BusinessModel.Course;
using InstructorBO = STCS.Infrastructure.BusinessModel.Instructor;
using StudentBO = STCS.Infrastructure.BusinessModel.Student;

namespace STCS.Infrastructure.Profiles;

public class DataProfile : Profile
{
    public DataProfile()
    {
        CreateMap<Course, CourseBO>()
            .ReverseMap();
        CreateMap<Instructor, InstructorBO>()
            .ReverseMap();
        CreateMap<Student, StudentBO>()
            .ReverseMap();
    }
}
