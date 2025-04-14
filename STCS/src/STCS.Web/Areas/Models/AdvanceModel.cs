using Autofac;
using AutoMapper;
using STCS.Infrastructure.Models;
using STCS.Infrastructure.Services;

namespace STCS.Web.Areas.Models;

public class AdvanceModel
{
    private ICourseService? _courseService;
    private IMapper _mapper;
    
    public CourseSearch SearchItem { get; set; }

    public AdvanceModel()
    {
        SearchItem = new CourseSearch
        {
            ClassStartDateFrom = DateTime.Now,
            ClassStartDateTo = DateTime.Now
        };
    }
    public AdvanceModel(ICourseService courseService, IMapper mapper) : this()
    {
        _courseService = courseService;
        _mapper = mapper;
    }

    public void ResolveDependency(ILifetimeScope scope)
    {
        _courseService = scope.Resolve<ICourseService>();
        _mapper = scope.Resolve<IMapper>();
    }

    internal void DeleteCourse(Guid id)
    {
        _courseService?.DeleteCourse(id);
    }

    internal async Task<object> GetPagedCoursesAdvanced(DataTablesAjaxRequestModel dataTablesModel)
    {
        var data = await _courseService?.GetCoursesAdvanced(dataTablesModel.PageIndex,
                                                            dataTablesModel.PageSize,
                                                            SearchItem.CourseName,
                                                            SearchItem.ClassStartDateFrom,
                                                            SearchItem.ClassStartDateTo,
                                                            dataTablesModel.GetSortText(new string[] { "CourseName", "OIC", "JIC" }));

        return new
        {
            recordsTotal = data.total,
            recordsFiltered = data.totalDisplay,
            data = (from record in data.records
                    select new string[]
                    {
                                record.CourseName,
                                record.OIC.ToString(),
                                record.JIC.ToString(),
                                record.Id.ToString()
                    }
                ).ToArray()
        };
    }
}
