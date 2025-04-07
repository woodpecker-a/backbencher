using Microsoft.EntityFrameworkCore;
using STCS.Infrastructure.Entities;

namespace STCS.Infrastructure.DbContexts;

public interface IApplicationDbContext
{
    DbSet<Course> Courses { get; }
    DbSet<Instructor> Instructors { get; }
    DbSet<Student> Students { get; }
    DbSet<Subject> Subjects { get; }
    DbSet<Class> Classes { get; }
    DbSet<ClassFile> Files { get; }
}