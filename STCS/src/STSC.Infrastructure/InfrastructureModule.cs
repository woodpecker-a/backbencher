using Autofac;
using STCS.Infrastructure.DbContexts;
using STCS.Infrastructure.Repositories;
using STCS.Infrastructure.Services;
using STCS.Infrastructure.Services.Utilities;
using STCS.Infrastructure.UnitOfWorks;

namespace STCS.Infrastructure;

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

        builder.RegisterType<TimeService>().As<ITimeService>()
                .InstancePerLifetimeScope();

        builder.RegisterType<DataUtility>().As<IDataUtility>()
            .InstancePerLifetimeScope();

        builder.RegisterType<TokenService>().As<ITokenService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CourseRepository>().As<ICourseRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CourseService>().As<ICourseService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<StudentRepository>().As<IStudentRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<StudentService>().As<IStudentService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<InstructorRepository>().As<IInstructorRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<InstructorService>().As<IInstructorService>()
            .InstancePerLifetimeScope();

        base.Load(builder);
    }
}