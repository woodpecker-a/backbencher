using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using STCS.Infrastructure.BusinessModel;
using STCS.Infrastructure.Enum;
using STCS.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace STCS.Web.Models;

public class InstructorCreateModel : BaseModel
{
    [Required(ErrorMessage = "Title must be provided"),
        StringLength(200, ErrorMessage = "Title should be less than 200 characters")]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public IdInit IdInitial { get; set; }
    public int IdNo { get; set; }
    public Rank Rank { get; set; }
    public InstructorType? InstructorType { get; set; }
    public Guid? CourseId { get; set; }
    public IEnumerable<SelectListItem>? IdInitialList { get; set; }
    public IEnumerable<SelectListItem>? RankList { get; set; }
    public IEnumerable<SelectListItem>? InstructorTypeList { get; set; }

    private IInstructorService? _instructorService;
    private IMapper _mapper;

    public InstructorCreateModel() : base()
    {

    }

    public InstructorCreateModel(IInstructorService instructorService, IMapper mapper)
    {
        _instructorService = instructorService;
        _mapper = mapper;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _instructorService = _scope.Resolve<IInstructorService>();
        _mapper = _scope.Resolve<IMapper>();
    }

    internal async Task CreateInstructor()
    {
        Instructor instructor = _mapper.Map<Instructor>(this);

        _instructorService.CreateInstructor(instructor);
    }

    public void PopulateEnumLists()
    {
        IdInitialList = Enum.GetValues(typeof(IdInit))
                             .Cast<IdInit>()
                             .Select(e => new SelectListItem
                             {
                                 Text = e.ToString(),  // Enum name
                                 Value = ((int)e).ToString()  // Enum value (as string)
                             })
                             .ToList();

        RankList = Enum.GetValues(typeof(Rank))
                       .Cast<Rank>()
                       .Select(e => new SelectListItem
                       {
                           Text = e.ToString(),
                           Value = ((int)e).ToString()
                       })
                       .ToList();

        InstructorTypeList = Enum.GetValues(typeof(InstructorType))
                                        .Cast<InstructorType>()
                                        .Select(e => new SelectListItem
                                        {
                                            Text = e.ToString(),
                                            Value = ((int)e).ToString()
                                        })
                                        .ToList();
    }
}
