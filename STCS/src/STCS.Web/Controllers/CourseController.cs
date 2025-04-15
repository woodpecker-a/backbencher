using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STCS.Web.Models;
using STCS.Web.Utilities;
using System.Data;

namespace STCS.Web.Controllers;

[Authorize]
public class CourseController : Controller
{
    private readonly ILifetimeScope _scope;
    private readonly ILogger<CourseController> _logger;

    public CourseController(ILogger<CourseController> logger, ILifetimeScope scope)
    {
        _scope = scope;
        _logger = logger;
    }


    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Create()
    {
        CourseCreateModel model = _scope.Resolve<CourseCreateModel>();
        return View(model);
    }

    [Authorize(Policy = "CourseViewPolicy")]
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