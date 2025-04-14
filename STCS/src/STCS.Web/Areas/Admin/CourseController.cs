using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace STCS.Web.Areas.Admin;

public class CourseController : Controller
{
    private readonly ILifetimeScope? _scope;
    private readonly ILogger<CourseController>? _logger;
    public CourseController(ILogger<CourseController> logger, ILifetimeScope scope)
    {
        _scope = scope;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var model = _scope.Resolve<AdvanceModel>();
        return View();
    }
}
