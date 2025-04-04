namespace STCS.Infrastructure.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    void Save();
}