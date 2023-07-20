using DreamHomeWeb.Models;
using DreamHomeWeb.Models.Domain;
using DreamHomeWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamHomeWeb.Controllers;

public class PrivateOwnerController : Controller, IEntityController<PrivateOwner>
{
    private readonly RepositoryService _repositoryService;
    private readonly DomainModel _domainModel;

    public PrivateOwnerController(RepositoryService repositoryService, DomainModel domainModel)
    {
        _repositoryService = repositoryService;
        _domainModel = domainModel;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var privateOwners = await _repositoryService.GetAll<PrivateOwner>(ApiUrls.PrivateOwnerUrl);
        _domainModel.PrivateOwners = privateOwners == null ? new List<PrivateOwner>() : privateOwners.ToList();
        return View(_domainModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PrivateOwner privateOwner)
    {
        privateOwner.Id = 0;
        var createdOwner = await _repositoryService.Create<PrivateOwner>(ApiUrls.PrivateOwnerUrl, privateOwner);
        if (createdOwner == null)
        {
            ModelState.AddModelError("", "Failed to create the privateOwner.");
            return View(privateOwner);
        }

        return RedirectToAction("Details", new { id = createdOwner.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var privateOwner = await _repositoryService.GetById<PrivateOwner>(ApiUrls.PrivateOwnerUrl, id);
        if (privateOwner == null)
        {
            return RedirectToAction("Index");
        }

        return View(privateOwner);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, PrivateOwner privateOwner)
    {
        var isUpdatedOwner = await _repositoryService.Update(ApiUrls.PrivateOwnerUrl, id, privateOwner);
        if (isUpdatedOwner)
        {
            return RedirectToAction("Details", new { id });
        }

        ModelState.AddModelError("", "Failed to update the privateOwner.");
        return View(privateOwner);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var privateOwner = await _repositoryService.GetById<PrivateOwner>(ApiUrls.PrivateOwnerUrl, id);
        if (privateOwner == null)
        {
            return RedirectToAction("Index");
        }

        return View(privateOwner);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var privateOwner = await _repositoryService.GetById<PrivateOwner>(ApiUrls.PrivateOwnerUrl, id);
        if (privateOwner == null)
        {
            return RedirectToAction("Index");
        }

        return View(privateOwner);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, PrivateOwner privateOwner)
    {
        await _repositoryService.Delete(ApiUrls.PrivateOwnerUrl, id);
        return RedirectToAction("Index");
    }
}