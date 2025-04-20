using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STCS.Web.Models;
using STCS.Web.Utilities;
using System.Data;

namespace STCS.Web.Controllers;

[Authorize]
public class StudentController : Controller
{
    private readonly ILifetimeScope _scope;
    private readonly ILogger<StudentController> _logger;

    public StudentController(ILogger<StudentController> logger, ILifetimeScope scope)
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
        StudentCreateModel model = _scope.Resolve<StudentCreateModel>();
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StudentCreateModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);

            try
            {
                await model.CreateStudent();
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully added a new Student.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index");
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogError(ex, ex.Message);

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "There was a problem in creating Student.",
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
        var model = _scope.Resolve<StudentEditModel>();
        model.LoadData(id);

        return View(model);
    }

    [ValidateAntiForgeryToken, HttpPost]
    public IActionResult Edit(StudentEditModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);

            try
            {
                model.EditStudent();

                TempData["ResponseMessage"] = "Successfuly updated Student.";
                TempData["ResponseType"] = ResponseTypes.Success;

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData["ResponseMessage"] = "There was a problem in updating Student.";
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
            var model = _scope.Resolve<StudentListModel>();
            model.DeleteStudent(id);

            TempData["ResponseMessage"] = "Successfuly deleted Student.";
            TempData["ResponseType"] = ResponseTypes.Success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            TempData["ResponseMessage"] = "There was a problem in deleteing Student.";
            TempData["ResponseType"] = ResponseTypes.Danger;
        }

        return RedirectToAction("Index");
    }
}