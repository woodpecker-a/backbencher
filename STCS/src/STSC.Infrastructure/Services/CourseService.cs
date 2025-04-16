using AutoMapper;
using STCS.Infrastructure.UnitOfWorks;
using CourseBO = STCS.Infrastructure.BusinessModel.Course;
using CourseEO = STCS.Infrastructure.Entities.Course;

namespace STCS.Infrastructure.Services;

public class CourseService : ICourseService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    private readonly IMapper _mapper;

    public CourseService(IMapper mapper, IApplicationUnitOfWork applicationUnitOfWork)
    {
        _applicationUnitOfWork = applicationUnitOfWork;
        _mapper = mapper;
    }

    public void CreateCourse(CourseBO course)
    {
        var count = _applicationUnitOfWork.Courses.GetCount(x => x.CourseName == course.CourseName);

        if (count > 0)
            throw new Exception("Course title already exists");

        CourseEO courseEntity = _mapper.Map<CourseEO>(course);

        _applicationUnitOfWork.Courses.Add(courseEntity);
        _applicationUnitOfWork.Save();
    }

    public void DeleteCourse(Guid id)
    {
        _applicationUnitOfWork.Courses.Remove(id);
        _applicationUnitOfWork.Save();
    }

    public void EditCourse(CourseBO course)
    {
        var courseEO = _applicationUnitOfWork.Courses.GetById(course.Id);
        if (courseEO != null)
        {
            courseEO = _mapper.Map(course, courseEO);
            _applicationUnitOfWork.Save();
        }
        else
            throw new InvalidOperationException("Course was not found");
    }

    public CourseBO GetCourse(string name)
    {
        var courseEO = _applicationUnitOfWork.Courses.Get(x => x.CourseName.Equals(name), "")
        .FirstOrDefault();
        CourseBO courseBO = _mapper.Map<CourseBO>(courseEO);

        return courseBO;
    }

    public CourseBO GetCourse(Guid id)
    {
        var courseEO = _applicationUnitOfWork.Courses.GetById(id);

        CourseBO courseBO = _mapper.Map<CourseBO>(courseEO);

        return courseBO;
    }

    public (int total, int totalDisplay, IList<CourseBO> records) GetCourses(int pageIndex,
            int pageSize, string searchText, string orderby)
    {
        (IList<CourseEO> data, int total, int totalDisplay) results = _applicationUnitOfWork
            .Courses.GetCourses(pageIndex, pageSize, searchText, orderby);

        IList<CourseBO> courses = new List<CourseBO>();
        foreach (CourseEO courseEO in results.data)
        {
            courses.Add(_mapper.Map<CourseBO>(courseEO));
        }

        return (results.total, results.totalDisplay, courses);
    }

    public IList<CourseBO> GetCourses()
    {
        var coursesEO = _applicationUnitOfWork.Courses.GetAll();

        IList<CourseBO> courses = new List<CourseBO>();

        foreach (CourseEO courseEO in coursesEO)
        {
            courses.Add(_mapper.Map<CourseBO>(courseEO));
        }

        return courses;
    }
}
