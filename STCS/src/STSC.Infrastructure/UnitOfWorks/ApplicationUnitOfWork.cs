using Microsoft.EntityFrameworkCore;
using STCS.Infrastructure.DbContexts;
using STCS.Infrastructure.Repositories;

namespace STCS.Infrastructure.UnitOfWorks;

public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
{
    public ICourseRepository Courses { get; private set; }
    public IInstructorRepository Instructors { get; private set; }
    public IStudentRepository Students { get; private set; }

    public ApplicationUnitOfWork(IApplicationDbContext dbContext,
        ICourseRepository courseRepository,
        IInstructorRepository instructorRepository,
        IStudentRepository studentRepository) : base((DbContext)dbContext)
    {
        Courses = courseRepository;
        Instructors = instructorRepository;
        Students = studentRepository;
    }
}
