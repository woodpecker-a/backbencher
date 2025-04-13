using STCS.Infrastructure.Entities;

namespace STCS.Infrastructure.Repositories;

public interface ICourseRepository : IRepository<Course, Guid>
{
    Task<(IList<Course> data, int total, int totalDisplay)> GetCoursesWithAdvanceSearch(int pageIndex,
            int pageSize, string title, DateTime classStartDateFrom, DateTime classStartDateTo,
            string orderby);
}