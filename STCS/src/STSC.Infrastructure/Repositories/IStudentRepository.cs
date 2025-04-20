using STCS.Infrastructure.Entities;

namespace STCS.Infrastructure.Repositories;

public interface IStudentRepository : IRepository<Student, Guid>
{
    (IList<Student> data, int total, int totalDisplay) GetStudents(int pageIndex,
            int pageSize, string searchText, string orderby);
}
