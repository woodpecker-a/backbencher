using Microsoft.EntityFrameworkCore;
using STCS.Infrastructure.Entities;

namespace STCS.Infrastructure.DbContexts;

public interface IApplicationDbContext
{
    DbSet<Course> Courses { get; set; }
    DbSet<Instructor> Instructors { get; set; }
    DbSet<Student> Students { get; set; }
    DbSet<Subject> Subjects { get; set; }
    DbSet<Class> Classes { get; set; }
    DbSet<ClassFile> Files { get; set; }
}