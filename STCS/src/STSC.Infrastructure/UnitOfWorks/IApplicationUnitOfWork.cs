using STCS.Infrastructure.Repositories;

namespace STCS.Infrastructure.UnitOfWorks;

public interface IApplicationUnitOfWork : IUnitOfWork
{
    ICourseRepository Courses { get; }
    IInstructorRepository Instructors { get; }
    IStudentRepository Students { get; }
}