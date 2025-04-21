using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        var course = _courseService.GetCourse(id);
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
