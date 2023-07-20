using DreamHomeWeb.Models;
using DreamHomeWeb.Models.Domain;
using DreamHomeWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamHomeWeb.Controllers;

public class StaffController : Controller, IEntityController<Staff>
{
    private readonly RepositoryService _repositoryService;
    private readonly DomainModel _domainModel;

    public StaffController(RepositoryService repositoryService, DomainModel domainModel)
    {
        _repositoryService = repositoryService;
        _domainModel = domainModel;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var staff = await _repositoryService.GetAll<Staff>(ApiUrls.StaffUrl);
        _domainModel.Staff = staff == null ? new List<Staff>() : staff.ToList();
        return View(_domainModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Staff staff)
    {
        staff.Id = 0;
        var createdStaff = await _repositoryService.Create<Staff>(ApiUrls.StaffUrl, staff);
        if (createdStaff == null)
        {
            ModelState.AddModelError("", "Failed to create the staff.");
            return View(staff);
        }

        return RedirectToAction("Details", new { id = createdStaff.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var staff = await _repositoryService.GetById<Staff>(ApiUrls.StaffUrl, id);
        if (staff == null)
        {
            return RedirectToAction("Index");
        }

        return View(staff);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Staff staff)
    {
        var isUpdatedStaff = await _repositoryService.Update(ApiUrls.StaffUrl, id, staff);
        if (isUpdatedStaff)
        {
            return RedirectToAction("Details", new { id });
        }

        ModelState.AddModelError("", "Failed to update the staff.");
        return View(staff);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var staff = await _repositoryService.GetById<Staff>(ApiUrls.StaffUrl, id);
        if (staff == null)
        {
            return RedirectToAction("Index");
        }

        return View(staff);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var staff = await _repositoryService.GetById<Staff>(ApiUrls.StaffUrl, id);
        if (staff == null)
        {
            return RedirectToAction("Index");
        }

        return View(staff);
    }
    

    [HttpPost]
    public async Task<IActionResult> Delete(int id, Staff staff)
    {
        await _repositoryService.Delete(ApiUrls.StaffUrl, id);
        return RedirectToAction("Index");
    }
}