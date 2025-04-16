using Autofac;
using STCS.Infrastructure.Models;
using STCS.Infrastructure.Services;

namespace STCS.Web.Models;

public class CourseListModel : BaseModel
{
    private ICourseService? _courseService;

    public CourseListModel(ICourseService coursService)
    {
        _courseService = coursService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _courseService = _scope.Resolve<ICourseService>();
    }

    internal void DeleteCourse(Guid id)
    {
        _courseService?.DeleteCourse(id);
    }

    internal Task<object> GetAllCourse(DataTablesAjaxRequestModel requestModel)
    {
        var data = _courseService.GetCourses(
        requestModel.PageIndex,
        requestModel.PageSize,
        requestModel.SearchText,
        requestModel.GetSortText(new string[] { "CourseName", "CourseCode", "CourseStartDate", "OIC", "JIC" }));

        return new
        {
            recordsTotal = data.total,
            recordsFiltered = data.totalDisplay,
            data = data.records.Select(record => new string[]
            {
                record.CourseName,
                record.CourseCode.ToString(),
                record.CourseStartDate.ToLongDateString(),
                record.OIC.FirstName,
                record.JIC.FirstName,
                record.Students.Count.ToString()
            ) }.ToArray();
        }
    }