using Microsoft.AspNetCore.Mvc;

namespace STCS.Web.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
