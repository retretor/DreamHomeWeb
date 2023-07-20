using DreamHomeWeb.Models;
using DreamHomeWeb.Models.Domain;
using DreamHomeWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamHomeWeb.Controllers;

public class ViewingController : Controller, IEntityController<Viewing>
{
    private readonly RepositoryService _repositoryService;
    private readonly DomainModel _domainModel;

    public ViewingController(RepositoryService repositoryService, DomainModel domainModel)
    {
        _repositoryService = repositoryService;
        _domainModel = domainModel;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var viewings = await _repositoryService.GetAll<Viewing>(ApiUrls.ViewingUrl);
        _domainModel.Viewings = viewings == null ? new List<Viewing>() : viewings.ToList();
        return View(_domainModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Viewing viewing)
    {
        viewing.Id = 0;
        var createdViewing = await _repositoryService.Create<Viewing>(ApiUrls.ViewingUrl, viewing);
        if (createdViewing == null)
        {
            ModelState.AddModelError("", "Failed to create the viewing.");
            return View(viewing);
        }

        return RedirectToAction("Details", new { id = createdViewing.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var viewing = await _repositoryService.GetById<Viewing>(ApiUrls.ViewingUrl, id);
        if (viewing == null)
        {
            return RedirectToAction("Index");
        }

        return View(viewing);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Viewing viewing)
    {
        var isUpdatedViewing = await _repositoryService.Update<Viewing>(ApiUrls.ViewingUrl, id, viewing);
        if (isUpdatedViewing)
        {
            return RedirectToAction("Details", new { id });
        }

        ModelState.AddModelError("", "Failed to update the viewing.");
        return View(viewing);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var viewing = await _repositoryService.GetById<Viewing>(ApiUrls.ViewingUrl, id);
        if (viewing == null)
        {
            return RedirectToAction("Index");
        }

        return View(viewing);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var viewing = await _repositoryService.GetById<Viewing>(ApiUrls.ViewingUrl, id);
        if (viewing == null)
        {
            return RedirectToAction("Index");
        }

        return View(viewing);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, Viewing viewing)
    {
        await _repositoryService.Delete(ApiUrls.ViewingUrl, id);
        return RedirectToAction("Index");
    }
}