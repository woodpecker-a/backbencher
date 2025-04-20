using STCS.Infrastructure.Entities;

namespace STCS.Infrastructure.Repositories;

public interface IInstructorRepository : IRepository<Instructor, Guid>
{
    (IList<Instructor> data, int total, int totalDisplay) GetInstructors(int pageIndex,
            int pageSize, string searchText, string orderby);
}