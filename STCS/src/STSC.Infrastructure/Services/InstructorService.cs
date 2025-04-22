using AutoMapper;
using STCS.Infrastructure.UnitOfWorks;
using InstructorBO = STCS.Infrastructure.BusinessModel.Instructor;
using InstructorEO = STCS.Infrastructure.Entities.Instructor;

namespace STCS.Infrastructure.Services;

public class InstructorService : IInstructorService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    private readonly IMapper _mapper;

    public InstructorService(IMapper mapper, IApplicationUnitOfWork applicationUnitOfWork)
    {
        _applicationUnitOfWork = applicationUnitOfWork;
        _mapper = mapper;
    }

    public void CreateInstructor(InstructorBO instructor)
    {
        var count = _applicationUnitOfWork.Instructors.GetCount(x => x.FirstName == instructor.FirstName);

        if (count > 0)
            throw new Exception("Instructor title already exists");

        InstructorEO instructorEntity = _mapper.Map<InstructorEO>(instructor);

        _applicationUnitOfWork.Instructors.Add(instructorEntity);
        _applicationUnitOfWork.Save();
    }

    public void DeleteInstructor(Guid id)
    {
        _applicationUnitOfWork.Instructors.Remove(id);
        _applicationUnitOfWork.Save();
    }

    public void EditInstructor(InstructorBO instructor)
    {
        var instructorEO = _applicationUnitOfWork.Instructors.GetById(instructor.Id);
        if (instructorEO != null)
        {
            instructorEO = _mapper.Map(instructor, instructorEO);
            _applicationUnitOfWork.Save();
        }
        else
            throw new InvalidOperationException("Instructor was not found");
    }

    public InstructorBO GetInstructor(string name)
    {
        var InstructorEO = _applicationUnitOfWork.Instructors.Get(x => x.FirstName.Equals(name), "")
        .FirstOrDefault();
        InstructorBO InstructorBO = _mapper.Map<InstructorBO>(InstructorEO);

        return InstructorBO;
    }

    public InstructorBO GetInstructor(Guid id)
    {
        var InstructorEO = _applicationUnitOfWork.Instructors.GetById(id);

        InstructorBO InstructorBO = _mapper.Map<InstructorBO>(InstructorEO);

        return InstructorBO;
    }

    public (int total, int totalDisplay, IList<InstructorBO> records) GetInstructors(int pageIndex,
            int pageSize, string searchText, string orderby)
    {
        (IList<InstructorEO> data, int total, int totalDisplay) results = _applicationUnitOfWork
            .Instructors.GetInstructors(pageIndex, pageSize, searchText, orderby);

        IList<InstructorBO> instructor = new List<InstructorBO>();
        foreach (InstructorEO instructorEO in results.data)
        {
            if(instructorEO.Course == null)
            {
                instructorEO.Course.CourseName = "N/A";
            }
            instructor.Add(_mapper.Map<InstructorBO>(instructorEO));
        }

        return (results.total, results.totalDisplay, instructor);
    }

    public IList<InstructorBO> GetInstructors()
    {
        var instructorsEO = _applicationUnitOfWork.Instructors.GetAll();

        IList<InstructorBO> instructors = new List<InstructorBO>();

        foreach (InstructorEO instructor in instructorsEO)
        {
            instructors.Add(_mapper.Map<InstructorBO>(instructor));
        }

        return instructors;
    }
}
