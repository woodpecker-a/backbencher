using STCS.Infrastructure.BusinessModel;

namespace STCS.Infrastructure.Services;

public interface IStudentService
{
    void CreateStudent(Student Student);
    (int total, int totalDisplay, IList<Student> records) GetStudents(int pageIndex, int pageSize, string searchText, string orderby);
    void DeleteStudent(Guid id);
    void EditStudent(Student Student);
    IList<Student> GetStudents();
    Student GetStudent(string name);
    Student GetStudent(Guid id);
}