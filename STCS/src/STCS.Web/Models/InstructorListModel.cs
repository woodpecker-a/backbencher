using STCS.Infrastructure.Models;
using STCS.Infrastructure.Services;

namespace STCS.Web.Models;

public class InstructorListModel : BaseModel
{
    private readonly IInstructorService _instructorService;

    // Constructor injection works fine, no need to manually resolve it again
    public InstructorListModel(IInstructorService instructorService)
    {
        _instructorService = instructorService ?? throw new ArgumentNullException(nameof(instructorService));
    }

    internal void DeleteInstructor(Guid id)
    {
        _instructorService.DeleteInstructor(id);
    }

    internal async Task<object> GetAllInstructor(DataTablesAjaxRequestModel requestModel)
    {
        var data = _instructorService.GetInstructors(
            requestModel.PageIndex,
            requestModel.PageSize,
            requestModel.SearchText,
            requestModel.GetSortText(new[] { "FirstName", "LastName", "IdInitial", "IdNo", "Rank", "InstructorType", "Course" })
        );

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
                record.InstructorType.ToString(),
                record.Course.CourseName.ToString() ?? "N/A",
                record.Id.ToString()
            }).ToArray()
        };

        return result;
    }
}
