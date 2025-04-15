using Autofac;
using AutoMapper;
using STCS.Infrastructure.BusinessModel;
using STCS.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace STCS.Web.Models;

public class CourseEditModel : BaseModel
{
    private ICourseService _courseService;
    private IMapper _mapper;

    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public double Fees { get; set; }
    public DateTime ClassStartDate { get; set; }

    public CourseEditModel() : base()
    {

    }

    public CourseEditModel(ICourseService coursService, IMapper mapper)
    {
        _courseService = coursService;
        _mapper = mapper;
    }

    internal void LoadData(Guid id)
    {
        var course = _courseService.GetCourses(id);
        if (course != null)
        {
            _mapper.Map(course, this);
        }
    }

    internal void EditCourse()
    {
        var course = _mapper.Map<Course>(this);
        _courseService.EditCourse(course);
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _courseService = _scope.Resolve<ICourseService>();
        _mapper = _scope.Resolve<IMapper>();
    }
}
