using AutoMapper;
using STCS.Infrastructure.UnitOfWorks;
using StudentBO = STCS.Infrastructure.BusinessModel.Student;
using StudentEO = STCS.Infrastructure.Entities.Student;

namespace STCS.Infrastructure.Services;

public class StudentService : IStudentService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    private readonly IMapper _mapper;

    public StudentService(IMapper mapper, IApplicationUnitOfWork applicationUnitOfWork)
    {
        _applicationUnitOfWork = applicationUnitOfWork;
        _mapper = mapper;
    }

    public void CreateStudent(StudentBO Student)
    {
        var count = _applicationUnitOfWork.Students.GetCount(x => x.FirstName == Student.FirstName);

        if (count > 0)
            throw new Exception("Student title already exists");

        StudentEO StudentEntity = _mapper.Map<StudentEO>(Student);

        _applicationUnitOfWork.Students.Add(StudentEntity);
        _applicationUnitOfWork.Save();
    }

    public void DeleteStudent(Guid id)
    {
        _applicationUnitOfWork.Students.Remove(id);
        _applicationUnitOfWork.Save();
    }

    public void EditStudent(StudentBO Student)
    {
        var StudentEO = _applicationUnitOfWork.Students.GetById(Student.Id);
        if (StudentEO != null)
        {
            StudentEO = _mapper.Map(Student, StudentEO);
            _applicationUnitOfWork.Save();
        }
        else
            throw new InvalidOperationException("Student was not found");
    }

    public StudentBO GetStudent(string name)
    {
        var StudentEO = _applicationUnitOfWork.Students.Get(x => x.FirstName.Equals(name), "")
        .FirstOrDefault();
        StudentBO StudentBO = _mapper.Map<StudentBO>(StudentEO);

        return StudentBO;
    }

    public StudentBO GetStudent(Guid id)
    {
        var StudentEO = _applicationUnitOfWork.Students.GetById(id);

        StudentBO StudentBO = _mapper.Map<StudentBO>(StudentEO);

        return StudentBO;
    }

    public (int total, int totalDisplay, IList<StudentBO> records) GetStudents(int pageIndex,
            int pageSize, string searchText, string orderby)
    {
        (IList<StudentEO> data, int total, int totalDisplay) results = _applicationUnitOfWork
            .Students.GetStudents(pageIndex, pageSize, searchText, orderby);

        IList<StudentBO> Students = new List<StudentBO>();
        foreach (StudentEO StudentEO in results.data)
        {
            Students.Add(_mapper.Map<StudentBO>(StudentEO));
        }

        return (results.total, results.totalDisplay, Students);
    }

    public IList<StudentBO> GetStudents()
    {
        var StudentsEO = _applicationUnitOfWork.Students.GetAll();

        IList<StudentBO> Students = new List<StudentBO>();

        foreach (StudentEO StudentEO in StudentsEO)
        {
            Students.Add(_mapper.Map<StudentBO>(StudentEO));
        }

        return Students;
    }
}
