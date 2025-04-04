namespace STSC.Infrastructure.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    void Save();
}