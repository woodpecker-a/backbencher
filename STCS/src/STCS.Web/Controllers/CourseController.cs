using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using STCS.Infrastructure.BusinessModel;
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

    public CourseController(ILogger<CourseController> logger,IInstructorService service, ILifetimeScope scope)
    {
        _scope = scope;
        _logger = logger;
        _service = service;
    }


    public IActionResult Index()
    {
        // Resolve the CourseListModel using Autofac (dependency injection)
        var model = _scope.Resolve<CourseListModel>();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> GetCourses(DataTablesAjaxRequestModel model)
    {
        try
        {
            // Resolve the model from Autofac and fetch course data
            var courseListModel = _scope.Resolve<CourseListModel>();

            // Use await for asynchronous method call
            var data = await courseListModel.GetAllCourse(model);

            return Json(data);
        }
        catch (Exception ex)
        {
            // Handle errors gracefully and return status code 500 with error details
            return StatusCode(500, ex.Message + "\n\n" + ex.StackTrace);
        }
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