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
            .HasForeignKey(c => c.EnrolledCourse);

        modelBuilder.Entity<Course>()
            .HasOne(i => i.OIC)
            .WithMany()
            .HasForeignKey(i => i.OICId);
        
        modelBuilder.Entity<Course>()
            .HasOne(i => i.JIC)
            .WithMany()
            .HasForeignKey(i => i.JICId);
        
        modelBuilder.Entity<Course>()
            .HasOne(i => i.NIC)
            .WithMany()
            .HasForeignKey(i => i.NICId);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Instructor> Instructors { get; set; }

    public DbSet<Student> Students { get; set; }

    public DbSet<Subject> Subjects { get; set; }

    public DbSet<Class> Classes { get; set; }

    public DbSet<ClassFile> Files { get; set; }
}