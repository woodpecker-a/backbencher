using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using STCS.Infrastructure.Models;
using STCS.Infrastructure.Services;
using STCS.Web.Models;
using STCS.Web.Utilities;
using System.Data;

namespace STCS.Web.Controllers;

[Authorize]
public class CourseController : Controller
{
    private readonly ILifetimeScope _scope;
    private readonly ILogger<CourseController> _logger;
    private readonly IInstructorService _service;

    public CourseController(ILogger<CourseController> logger, IInstructorService service, ILifetimeScope scope)
    {
        _scope = scope;
        _logger = logger;
        _service = service;
    }


    public IActionResult Index()
    {
        var model = _scope.Resolve<CourseListModel>();
        var dataTablesModel = new DataTablesAjaxRequestModel(Request);

        return View(model);
    }

    [HttpGet]
    public async Task<object> GetCourses()
    {
        var model = _scope.Resolve<CourseListModel>();

        var dataTablesModel = new DataTablesAjaxRequestModel(Request);
        model.ResolveDependency(_scope);

        var data = await model.GetAllCourse(dataTablesModel);
        return data;
    }

    [HttpPost]
    public IActionResult DeleteCourse(Guid id)
    {
        var model = _scope.Resolve<CourseListModel>();
        model.DeleteCourse(id);
        return Ok();
    }

    public IActionResult Create()
    {
        CourseCreateModel model = _scope.Resolve<CourseCreateModel>();
        var instructors = _service.GetInstructors().ToList();

        model.OICList = instructors.Select(i => new SelectListItem
        {
            Value = i.Id.ToString(),
            Text = i.FirstName // or any display property
        });

        model.JICList = instructors.Select(i => new SelectListItem
        {
            Value = i.Id.ToString(),
            Text = i.FirstName
        });

        model.NICList = instructors.Select(i => new SelectListItem
        {
            Value = i.Id.ToString(),
            Text = i.FirstName
        });

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseCreateModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);

            try
            {
                await model.CreateCourse();
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully added a new course.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index");
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogError(ex, ex.Message);

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "There was a problem in creating course.",
                    Type = ResponseTypes.Danger
                });
            }
        }
        else
        {
            string messageText = string.Empty;
            foreach (var message in ModelState.Values)
            {
                for (int i = 0; i < message.Errors.Count; i++)
                {
                    messageText += message.Errors[i].ErrorMessage;
                }
            }
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = messageText,
                Type = ResponseTypes.Danger
            });
        }
        return View(model);
    }

    public IActionResult Edit(Guid id)
    {
        var model = _scope.Resolve<CourseEditModel>();
        model.LoadData(id);

        return View(model);
    }

    [ValidateAntiForgeryToken, HttpPost]
    public IActionResult Edit(CourseEditModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);

            try
            {
                model.EditCourse();

                TempData["ResponseMessage"] = "Successfuly updated course.";
                TempData["ResponseType"] = ResponseTypes.Success;

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData["ResponseMessage"] = "There was a problem in updating course.";
                TempData["ResponseType"] = ResponseTypes.Danger;
            }
        }

        return View(model);
    }

    [ValidateAntiForgeryToken, HttpPost]
    public IActionResult Delete(Guid id)
    {
        try
        {
            var model = _scope.Resolve<CourseListModel>();
            model.DeleteCourse(id);

            TempData["ResponseMessage"] = "Successfuly deleted course.";
            TempData["ResponseType"] = ResponseTypes.Success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            TempData["ResponseMessage"] = "There was a problem in deleteing course.";
            TempData["ResponseType"] = ResponseTypes.Danger;
        }

        return RedirectToAction("Index");
    }
}