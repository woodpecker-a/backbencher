using Autofac;

public class InfrastructureModule : Module
{
    private readonly string _connectionString;
    private readonly string _migrationAssemblyName;

    public InfrastructureModule(string connectionString, string migrationAssemblyName)
    {
        _connectionString = connectionString;
        _migrationAssemblyName = migrationAssemblyName;
    }

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
    }
}