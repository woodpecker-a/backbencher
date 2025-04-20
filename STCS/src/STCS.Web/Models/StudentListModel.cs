using Autofac;
using STCS.Infrastructure.Models;
using STCS.Infrastructure.Services;

namespace STCS.Web.Models;

public class StudentListModel : BaseModel
{
    private IStudentService? _StudentService;

    public StudentListModel(IStudentService coursService)
    {
        _StudentService = coursService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _StudentService = _scope.Resolve<IStudentService>();
    }

    internal void DeleteStudent(Guid id)
    {
        _StudentService?.DeleteStudent(id);
    }

    internal Task<object> GetAllStudent(DataTablesAjaxRequestModel requestModel)
    {
        var data = _StudentService.GetStudents(
            requestModel.PageIndex,
            requestModel.PageSize,
            requestModel.SearchText,
            requestModel.GetSortText(new string[] { "FirstName", "LastName","Rank"}));

        var result = new
        {
            recordsTotal = data.total,
            recordsFiltered = data.totalDisplay,
            data = data.records.Select(record => new string[]
            {
                record.FirstName,
                record.LastName,
                record.IdInitial.ToString(),
                record.IdNo.ToString(),
                record.Rank.ToString(),
                record.EnrolledCourse.CourseName
            }).ToArray()
        };

        return Task.FromResult<object>(result);
    }
}