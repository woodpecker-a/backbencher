using AutoMapper;
using STCS.Infrastructure.Entities;
using CourseBO = STCS.Infrastructure.BusinessModel.Course;

namespace STCS.Infrastructure.Profiles;

public class DataProfile : Profile
{
    public DataProfile()
    {
        CreateMap<Course, CourseBO>()
            .ReverseMap();
    }
}
