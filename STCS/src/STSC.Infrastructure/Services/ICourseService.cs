using STCS.Infrastructure.BusinessModel;

namespace STCS.Infrastructure.Services;

public interface ICourseService
{
    void CreateCourse(Course course);
    Task<(int total, int totalDisplay, IList<Course> records)> GetCoursesAdvanced(int pageIndex,
            int pageSize, string title, DateTime classStartDateFrom,
            DateTime classStartDateTo, string orderby);
    void DeleteCourse(Guid id);
    Course GetCourses(Guid id);
    void EditCourse(Course course);
    IList<Course> GetCourses();
    Course GetCourse(string name);
    Course GetCourse(Guid id);
}