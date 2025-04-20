using Microsoft.EntityFrameworkCore;
using STCS.Infrastructure.DbContexts;
using STCS.Infrastructure.Entities;

namespace STCS.Infrastructure.Repositories;

public class CourseRepository : Repository<Course, Guid>, ICourseRepository
{
    public CourseRepository(IApplicationDbContext context) : base((DbContext)context)
    {
    }
    public (IList<Course> data, int total, int totalDisplay) GetCourses(int pageIndex,
            int pageSize, string searchText, string orderby)
    {
        (IList<Course> data, int total, int totalDisplay) results =
            GetDynamic(x => x.CourseName.Contains(searchText), orderby,
            "CourseCode,CourseStartDate,OIC,JIC", pageIndex, pageSize, true);

        return results;
    }
}