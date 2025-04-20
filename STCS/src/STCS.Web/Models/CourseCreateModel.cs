using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using STCS.Infrastructure.BusinessModel;
using STCS.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace STCS.Web.Models;
public class CourseCreateModel : BaseModel
{
    [Required(ErrorMessage = "Title must be provided"),
        StringLength(200, ErrorMessage = "Title should be less than 200 characters")]
    public string CourseName { get; set; }
    public string CourseCode { get; set; }
    public DateTime CourseStartDate { get; set; }
    public int CourseDuration { get; set; }
    public Guid OICId { get; set; }
    public Instructor? OIC { get; set; }
    public Guid JICId { get; set; }
    public Instructor? JIC { get; set; }
    public Guid NICId { get; set; }
    public Instructor? NIC { get; set; }
    public bool IsCompleted { get; set; }
    public IEnumerable<SelectListItem>? OICList { get; set; }
    public IEnumerable<SelectListItem>? JICList { get; set; }
    public IEnumerable<SelectListItem>? NICList { get; set; }

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
