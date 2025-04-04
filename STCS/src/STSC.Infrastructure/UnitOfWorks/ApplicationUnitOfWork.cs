using Microsoft.EntityFrameworkCore;
using STCS.Infrastructure.DbContexts;

namespace STCS.Infrastructure.UnitOfWorks;

public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
{
    public ApplicationUnitOfWork(IApplicationDbContext dbContext) : base((DbContext)dbContext)
    {

    }
}
