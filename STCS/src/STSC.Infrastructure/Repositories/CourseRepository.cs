using Microsoft.EntityFrameworkCore;
using STCS.Infrastructure.DbContexts;
using STCS.Infrastructure.Entities;

namespace STCS.Infrastructure.Repositories;

public class CourseRepository : Repository<Course, Guid>, ICourseRepository
{
    public CourseRepository(IApplicationDbContext context) : base((DbContext)context)
    {
    }
    public async Task<(IList<Course> data, int total, int totalDisplay)> GetCoursesWithAdvanceSearch(int pageIndex, int pageSize, string title, DateTime classStartDateFrom, DateTime classStartDateTo, string orderby)
    {
        var result = await QueryWithStoredProcedureAsync<Course>("GetCourses", new Dictionary<string, object>
            {
                {"Title", title},
                {"ClassStartDateFrom", classStartDateFrom},
                {"ClassStartDateTo", classStartDateTo},
                {"PageIndex", pageIndex},
                {"PageSize", pageSize},
                {"OrderBy", orderby }
            },
            new Dictionary<string, Type>
            {
                {"Total", typeof(int)},
                {"TotalDisplay", typeof(int)}
            });

        return (result.result, int.Parse(result.outValues.ElementAt(0).Value.ToString()), int.Parse(result.outValues.ElementAt(1).Value.ToString()));
    }
}
