using System.Data;
using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using STCS.Infrastructure.Models;
using STCS.Infrastructure.Services;
using STCS.Web.Models;
using STCS.Web.Utilities;

namespace STCS.Web.Controllers;

[Authorize]
public class InstructorController : Controller
{
    private readonly ILifetimeScope _scope;
    private readonly ILogger<InstructorController> _logger;
    private readonly ICourseService _service;
    public InstructorController(ILifetimeScope scope, ILogger<InstructorController> logger, ICourseService service)
    {
        _scope = scope;
        _logger = logger;
        _service = service;
    }
    public IActionResult Index()
    {
        var model = _scope.Resolve<InstructorListModel>();
        var dataTableModel = new DataTablesAjaxRequestModel(Request);

        return View();
    }

    [HttpGet]
    public async Task<object> GetInstructors()
    {
        var model = _scope.Resolve<InstructorListModel>();
        var dataTablesModel = new DataTablesAjaxRequestModel(Request);
        model.ResolveDependency(_scope);

        var data = await model.GetAllInstructor(dataTablesModel);
        return data;
    }

    public IActionResult Create()
    {
        InstructorCreateModel model = _scope.Resolve<InstructorCreateModel>();
        model.PopulateEnumLists();
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InstructorCreateModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);

            try
            {
                await model.CreateInstructor();
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully added a new Instructor.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index");
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogError(ex, ex.Message);

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "There was a problem in creating Instructor.",
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
}
