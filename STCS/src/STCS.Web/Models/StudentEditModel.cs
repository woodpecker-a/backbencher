using Autofac;
using AutoMapper;
using STCS.Infrastructure.BusinessModel;
using STCS.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace STCS.Web.Models;

public class StudentEditModel : BaseModel
{
    private IStudentService _StudentService;
    private IMapper _mapper;

    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public double Fees { get; set; }
    public DateTime ClassStartDate { get; set; }

    public StudentEditModel() : base()
    {

    }

    public StudentEditModel(IStudentService coursService, IMapper mapper)
    {
        _StudentService = coursService;
        _mapper = mapper;
    }

    internal void LoadData(Guid id)
    {
        var Student = _StudentService.GetStudent(id);
        if (Student != null)
        {
            _mapper.Map(Student, this);
        }
    }

    internal void EditStudent()
    {
        var Student = _mapper.Map<Student>(this);
        _StudentService.EditStudent(Student);
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _StudentService = _scope.Resolve<IStudentService>();
        _mapper = _scope.Resolve<IMapper>();
    }
}
