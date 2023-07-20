using DreamHomeWeb.Models;
using DreamHomeWeb.Models.Domain;
using DreamHomeWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamHomeWeb.Controllers;

public class BranchController : Controller, IEntityController<Branch>
{
    private readonly RepositoryService _repositoryService;
    private readonly DomainModel _domainModel;

    public BranchController(RepositoryService repositoryService, DomainModel domainModel)
    {
        _repositoryService = repositoryService;
        _domainModel = domainModel;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var branches = await _repositoryService.GetAll<Branch>(ApiUrls.BranchUrl);
        _domainModel.Branches = branches == null ? new List<Branch>() : branches.ToList();
        return View(_domainModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Branch branch)
    {
        branch.Id = 0;
        var createdBranch = await _repositoryService.Create<Branch>(ApiUrls.BranchUrl, branch);
        if (createdBranch == null)
        {
            ModelState.AddModelError("", "Failed to create the branch.");
            return View(branch);
        }

        return RedirectToAction("Details", new { id = createdBranch.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var branch = await _repositoryService.GetById<Branch>(ApiUrls.BranchUrl, id);
        if (branch == null)
        {
            return RedirectToAction("Index");
        }
        
        return View(branch);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Branch branch)
    {
        var isUpdatedBranch = await _repositoryService.Update<Branch>(ApiUrls.BranchUrl, id, branch);
        if (isUpdatedBranch)
        {
            return RedirectToAction("Details", new { id });
        }
        ModelState.AddModelError("", "Failed to update the branch.");
        return View(branch);

    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var branch = await _repositoryService.GetById<Branch>(ApiUrls.BranchUrl, id);
        if (branch == null)
        {
            return RedirectToAction("Index");
        }
        return View(branch);

    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var branch = await _repositoryService.GetById<Branch>(ApiUrls.BranchUrl, id);
        if (branch == null)
        {
            return RedirectToAction("Index");
        }

        return View(branch);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id, Branch branch)
    {
        await _repositoryService.Delete(ApiUrls.BranchUrl, id);
        return RedirectToAction("Index");
    }
}