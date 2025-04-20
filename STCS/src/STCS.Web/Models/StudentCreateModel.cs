using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using STCS.Infrastructure.BusinessModel;
using STCS.Infrastructure.Enum;
using STCS.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace STCS.Web.Models;

public class StudentCreateModel : BaseModel
{
    [Required(ErrorMessage = "Title must be provided"),
        StringLength(200, ErrorMessage = "Title should be less than 200 characters")]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public IdInit IdInitial { get; set; }
    public int? IdNo { get; set; }
    public Rank Rank { get; set; }
    public Guid CourseId { get; set; }
    public Course? EnrolledCourse { get; set; }
    public IEnumerable<SelectListItem> Courses { get; set; }

    private IStudentService? _StudentService;
    private IMapper _mapper;

    public StudentCreateModel() : base()
    {

    }

    public StudentCreateModel(IStudentService coursService, IMapper mapper)
    {
        _StudentService = coursService;
        _mapper = mapper;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _StudentService = _scope.Resolve<IStudentService>();
        _mapper = _scope.Resolve<IMapper>();
    }

    internal async Task CreateStudent()
    {
        Student Student = _mapper.Map<Student>(this);

        _StudentService.CreateStudent(Student);
    }
}
