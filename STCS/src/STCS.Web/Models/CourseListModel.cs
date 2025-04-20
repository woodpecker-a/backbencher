using STCS.Infrastructure.Models;
using STCS.Infrastructure.Services;

namespace STCS.Web.Models;

public class CourseListModel : BaseModel
{
    private readonly ICourseService _courseService;

    // Constructor injection works fine, no need to manually resolve it again
    public CourseListModel(ICourseService courseService)
    {
        _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
    }

    internal void DeleteCourse(Guid id)
    {
        _courseService.DeleteCourse(id);
    }

    internal Task<object> GetAllCourse(DataTablesAjaxRequestModel requestModel)
    {
        var data = _courseService.GetCourses(
            requestModel.PageIndex,
            requestModel.PageSize,
            requestModel.SearchText,
            requestModel.GetSortText(new[] { "CourseName", "CourseCode", "CourseStartDate", "OIC", "JIC" })
        );

        var result = new
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
                record.Students.Count.ToString(),
                record.IsCompleted.ToString(),
                record.Id.ToString()
            }).ToArray()
        };

        return Task.FromResult<object>(result);
    }
}
