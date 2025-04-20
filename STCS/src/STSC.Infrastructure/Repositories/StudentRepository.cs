using Microsoft.EntityFrameworkCore;
using STCS.Infrastructure.DbContexts;
using STCS.Infrastructure.Entities;

namespace STCS.Infrastructure.Repositories;

public class StudentRepository : Repository<Student, Guid>, IStudentRepository
{
    public StudentRepository(IApplicationDbContext context) : base((DbContext)context)
    {
    }
    public (IList<Student> data, int total, int totalDisplay) GetStudents(int pageIndex,
            int pageSize, string searchText, string orderby)
    {
        (IList<Student> data, int total, int totalDisplay) results =
            GetDynamic(x => x.FirstName.Contains(searchText), orderby,
            "FirstName,Rank,EnrolledCourse", pageIndex, pageSize, true);

        return results;
    }
}
