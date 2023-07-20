using DreamHomeWeb.Models;
using DreamHomeWeb.Models.Domain;
using DreamHomeWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamHomeWeb.Controllers;

public class PropertyForRentController : Controller, IEntityController<PropertyForRent>
{
    private readonly RepositoryService _repositoryService;
    private readonly DomainModel _domainModel;

    public PropertyForRentController(RepositoryService repositoryService,
        DomainModel domainModel)
    {
        _repositoryService = repositoryService;
        _domainModel = domainModel;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var properties = await _repositoryService.GetAll<PropertyForRent>(ApiUrls.PropertyUrl);
        _domainModel.Properties = properties == null ? new List<PropertyForRent>() : properties.ToList();
        return View(_domainModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PropertyForRent property)
    {
        property.Id = 0;
        var createdProperty = await _repositoryService.Create<PropertyForRent>(ApiUrls.PropertyUrl, property);
        if (createdProperty == null)
        {
            ModelState.AddModelError("", "Failed to create the property.");
            return View(property);
        }

        return RedirectToAction("Details", new { id = createdProperty.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var property = await _repositoryService.GetById<PropertyForRent>(ApiUrls.PropertyUrl, id);
        if (property == null)
        {
            return RedirectToAction("Index");
        }

        return View(property);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, PropertyForRent property)
    {
        var isUpdated = await _repositoryService.Update(ApiUrls.PropertyUrl, id, property);
        if (isUpdated)
        {
            return RedirectToAction("Details", new { id });
        }
        ModelState.AddModelError("", "Failed to update the property.");
        return View(property);

    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var property = await _repositoryService.GetById<PropertyForRent>(ApiUrls.PropertyUrl, id);
        if (property == null)
        {
            return RedirectToAction("Index");
        }

        return View(property);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var property = await _repositoryService.GetById<PropertyForRent>(ApiUrls.PropertyUrl, id);
        if (property == null)
        {
            return RedirectToAction("Index");
        }

        return View(property);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, PropertyForRent property)
    {
        await _repositoryService.Delete(ApiUrls.PropertyUrl, id);
        return RedirectToAction("Index");
    }
}