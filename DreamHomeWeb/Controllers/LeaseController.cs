using DreamHomeWeb.Models;
using DreamHomeWeb.Models.Domain;
using DreamHomeWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamHomeWeb.Controllers;

public class LeaseController : Controller, IEntityController<Lease>
{
private readonly RepositoryService _repositoryService;
    private readonly DomainModel _domainModel;

    public LeaseController(RepositoryService repositoryService, DomainModel domainModel)
    {
        _repositoryService = repositoryService;
        _domainModel = domainModel;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var leases = await _repositoryService.GetAll<Lease>(ApiUrls.LeaseUrl);
        _domainModel.Leases = leases == null ? new List<Lease>() : leases.ToList();
        return View(_domainModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Lease lease)
    {
        lease.Id = 0;
        var createdLease = await _repositoryService.Create<Lease>(ApiUrls.LeaseUrl, lease);
        if (createdLease == null)
        {
            ModelState.AddModelError("", "Failed to create the lease.");
            return View(lease);
        }

        return RedirectToAction("Details", new { id = createdLease.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var lease = await _repositoryService.GetById<Lease>(ApiUrls.LeaseUrl, id);
        if (lease == null)
        {
            return RedirectToAction("Index");
        }

        return View(lease);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Lease lease)
    {
        var isUpdatedLease = await _repositoryService.Update(ApiUrls.LeaseUrl, id, lease);
        if (isUpdatedLease)
        {
            return RedirectToAction("Details", new { id });
        }

        ModelState.AddModelError("", "Failed to update the lease.");
        return View(lease);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var lease = await _repositoryService.GetById<Lease>(ApiUrls.LeaseUrl, id);
        if (lease == null)
        {
            return RedirectToAction("Index");
        }

        return View(lease);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var lease = await _repositoryService.GetById<Lease>(ApiUrls.LeaseUrl, id);
        if (lease == null)
        {
            return RedirectToAction("Index");
        }

        return View(lease);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, Lease lease)
    {
        await _repositoryService.Delete(ApiUrls.LeaseUrl, id);
        return RedirectToAction("Index");
    }
}