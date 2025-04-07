using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using STCS.Infrastructure.Entities;
using STCS.Infrastructure.Entities.Applications;

namespace STCS.Infrastructure.DbContexts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
    ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>, IApplicationDbContext
{
    private readonly string _connectionString;
    private readonly string _assemblyName;

    public ApplicationDbContext(string connectionString, string assemblyName)
    {
        _connectionString = connectionString;
        _assemblyName = assemblyName;
    }

    public DbSet<Course> Courses;

    public DbSet<Instructor> Instructors;

    public DbSet<Student> Students;

    public DbSet<Subject> Subjects;

    public DbSet<Class> Classes;

    public DbSet<ClassFile> Files;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString,
                b => b.MigrationsAssembly(_assemblyName)
            );
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .HasMany(s => s.Students)
            .WithOne(c => c.EnrolledCourse)
            .HasForeignKey(c => c.Id);

        base.OnModelCreating(modelBuilder);
    }
}