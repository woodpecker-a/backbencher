using Microsoft.EntityFrameworkCore;
using STSC.Infrastructure.DbContexts;

namespace STSC.Infrastructure.UnitOfWorks;

public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
{
    public ApplicationUnitOfWork(IApplicationDbContext dbContext) : base((DbContext)dbContext)
    {

    }
}
