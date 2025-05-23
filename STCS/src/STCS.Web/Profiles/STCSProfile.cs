﻿using AutoMapper;
using STCS.Infrastructure.BusinessModel;
using STCS.Web.Models;

namespace STCS.Web.Profiles;

public class STCSProfile : Profile
{
    public STCSProfile()
    {
        CreateMap<Course, CourseCreateModel>()
            .ReverseMap();
        CreateMap<Course, CourseEditModel>()
            .ReverseMap();
        CreateMap<Instructor, InstructorCreateModel>()
            .ReverseMap();
        CreateMap<Student, StudentCreateModel>()
            .ReverseMap();
    }
}
