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

    public void CreateInstructor(InstructorBO Instructor)
    {
        var count = _applicationUnitOfWork.Instructors.GetCount(x => x.FirstName == Instructor.FirstName);

        if (count > 0)
            throw new Exception("Instructor title already exists");

        InstructorEO InstructorEntity = _mapper.Map<InstructorEO>(Instructor);

        _applicationUnitOfWork.Instructors.Add(InstructorEntity);
        _applicationUnitOfWork.Save();
    }

    public void DeleteInstructor(Guid id)
    {
        _applicationUnitOfWork.Instructors.Remove(id);
        _applicationUnitOfWork.Save();
    }

    public void EditInstructor(InstructorBO Instructor)
    {
        var InstructorEO = _applicationUnitOfWork.Instructors.GetById(Instructor.Id);
        if (InstructorEO != null)
        {
            InstructorEO = _mapper.Map(Instructor, InstructorEO);
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

        IList<InstructorBO> Instructors = new List<InstructorBO>();
        foreach (InstructorEO InstructorEO in results.data)
        {
            Instructors.Add(_mapper.Map<InstructorBO>(InstructorEO));
        }

        return (results.total, results.totalDisplay, Instructors);
    }

    public IList<InstructorBO> GetInstructors()
    {
        var InstructorsEO = _applicationUnitOfWork.Instructors.GetAll();

        IList<InstructorBO> Instructors = new List<InstructorBO>();

        foreach (InstructorEO InstructorEO in InstructorsEO)
        {
            Instructors.Add(_mapper.Map<InstructorBO>(InstructorEO));
        }

        return Instructors;
    }
}
