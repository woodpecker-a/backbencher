using Microsoft.EntityFrameworkCore;

namespace STSC.Infrastructure.UnitOfWorks;

public abstract class UnitOfWork : IUnitOfWork
{
    protected readonly DbContext _dbContext;

    protected UnitOfWork(DbContext dbContext) => _dbContext = dbContext;

    public void Dispose()
    {
        _dbContext?.Dispose();
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}
