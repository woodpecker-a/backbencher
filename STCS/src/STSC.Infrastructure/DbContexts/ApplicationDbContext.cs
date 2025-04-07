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
        // Course-Instructor Relationship (One-to-Many, each course has OIC, JIC, and NIC)
        modelBuilder.Entity<Course>()
            .HasOne(c => c.OIC)
            .WithMany()
            .HasForeignKey(c => c.OICId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.JIC)
            .WithMany()
            .HasForeignKey(c => c.JICId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.NIC)
            .WithMany()
            .HasForeignKey(c => c.NICId)
            .OnDelete(DeleteBehavior.Restrict);

        // Course-Student Relationship (Many-to-One)
        modelBuilder.Entity<Student>()
            .HasOne(s => s.EnrolledCourse)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.CourseId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete if a course is deleted

        // Class-course Relationship (One-to-Many)
        modelBuilder.Entity<Class>()
            .HasOne(c => c.Course)
            .WithMany(cl => cl.Classes)
            .HasForeignKey(cl => cl.CourseId);

        // Subject-ClassFile Relationship (One-to-Many)
        modelBuilder.Entity<Subject>()
            .HasMany(s => s.Files)
            .WithOne(cf => cf.Subject)
            .HasForeignKey(cf => cf.SubjectId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete if a subject is deleted

        // Instructor-Course Relationship (One-to-Many)
        modelBuilder.Entity<Instructor>()
            .HasOne(i => i.Course)
            .WithMany(c => c.Instructors)
            .HasForeignKey(i => i.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Instructor> Instructors { get; set; }

    public DbSet<Student> Students { get; set; }

    public DbSet<Subject> Subjects { get; set; }

    public DbSet<Class> Classes { get; set; }

    public DbSet<ClassFile> Files { get; set; }
}