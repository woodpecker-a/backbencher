using STCS.Infrastructure.BusinessModel;

namespace STCS.Infrastructure.Services;

public interface IInstructorService
{
    void CreateInstructor(Instructor Instructor);
    (int total, int totalDisplay, IList<Instructor> records) GetInstructors(int pageIndex, int pageSize, string searchText, string orderby);
    void DeleteInstructor(Guid id);
    void EditInstructor(Instructor Instructor);
    IList<Instructor> GetInstructors();
    Instructor GetInstructor(string name);
    Instructor GetInstructor(Guid id);
}
