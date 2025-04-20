using Microsoft.EntityFrameworkCore;
using STCS.Infrastructure.DbContexts;
using STCS.Infrastructure.Entities;

namespace STCS.Infrastructure.Repositories;

public class InstructorRepository : Repository<Instructor, Guid>, IInstructorRepository
{
    public InstructorRepository(IApplicationDbContext context) : base((DbContext)context)
    {
    }
    public (IList<Instructor> data, int total, int totalDisplay) GetInstructors(int pageIndex,
            int pageSize, string searchText, string orderby)
    {
        (IList<Instructor> data, int total, int totalDisplay) results =
            GetDynamic(x => x.FirstName.Contains(searchText), orderby,
            "FirstName,Rank,InstructorDesignation", pageIndex, pageSize, true);

        return results;
    }
}
