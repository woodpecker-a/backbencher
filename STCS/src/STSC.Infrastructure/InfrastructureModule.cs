using Autofac;
using STSC.Infrastructure.DbContexts;
using STSC.Infrastructure.UnitOfWorks;

namespace Infrastructure;

public class InfrastructureModule : Module
{
    private readonly string _connectionString;
    private readonly string _assemblyName;

    public InfrastructureModule(string connectionString, string assemblyName)
    {
        _connectionString = connectionString;
        _assemblyName = assemblyName;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("assemblyName", _assemblyName)
                .InstancePerLifetimeScope();

        builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("assemblyName", _assemblyName)
                .InstancePerLifetimeScope();

        builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();

        base.Load(builder);
    }
}