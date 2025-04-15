using Autofac;
using AutoMapper;
using STCS.Infrastructure.BusinessModel;
using STCS.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace STCS.Web.Models;

public class CourseCreateModel : BaseModel
{
    [Required(ErrorMessage = "Title must be provided"),
        StringLength(200, ErrorMessage = "Title should be less than 200 characters")]
    public string Title { get; set; }
    [Required, Range(1000, 50000)]
    public double Fees { get; set; }
    [Required(ErrorMessage = "Valid date must be provided")]
    public DateTime ClassStartDate { get; set; }

    private ICourseService? _courseService;
    private IMapper _mapper;

    public CourseCreateModel() : base()
    {

    }

    public CourseCreateModel(ICourseService coursService, IMapper mapper)
    {
        _courseService = coursService;
        _mapper = mapper;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _courseService = _scope.Resolve<ICourseService>();
        _mapper = _scope.Resolve<IMapper>();
    }

    internal async Task CreateCourse()
    {
        Course course = _mapper.Map<Course>(this);

        _courseService.CreateCourse(course);
    }
}
