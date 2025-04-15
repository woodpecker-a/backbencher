using Autofac;
using STCS.Infrastructure.Services;

namespace STCS.Web.Models;

public class CourseListModel : BaseModel
{
    private ICourseService? _courseService;

    public CourseListModel(ICourseService coursService)
    {
        _courseService = coursService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _courseService = _scope.Resolve<ICourseService>();
    }

    internal void DeleteCourse(Guid id)
    {
        _courseService?.DeleteCourse(id);
    }
}
