﻿using Autofac;
using STSC.Infrastructure.DbContexts;

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
        builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

        base.Load(builder);
    }
}