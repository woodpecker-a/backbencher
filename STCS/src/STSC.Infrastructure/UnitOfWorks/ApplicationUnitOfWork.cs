using Microsoft.EntityFrameworkCore;
using STCS.Infrastructure.DbContexts;
using STCS.Infrastructure.Repositories;

namespace STCS.Infrastructure.UnitOfWorks;

public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
{
    public ICourseRepository Courses { get; private set; }

    public ApplicationUnitOfWork(IApplicationDbContext dbContext,
        ICourseRepository courseRepository) : base((DbContext)dbContext)
    {
        Courses = courseRepository;
    }
}
