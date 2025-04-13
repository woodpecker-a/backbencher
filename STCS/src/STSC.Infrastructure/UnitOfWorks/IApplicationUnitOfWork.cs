using STCS.Infrastructure.Repositories;

namespace STCS.Infrastructure.UnitOfWorks;

public interface IApplicationUnitOfWork : IUnitOfWork
{
    ICourseRepository Courses { get; }
}