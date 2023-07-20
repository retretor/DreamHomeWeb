using DreamHomeWeb.Models;
using DreamHomeWeb.Models.Domain;
using DreamHomeWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamHomeWeb.Controllers;

public class ClientController : Controller, IEntityController<Client>
{
    private readonly RepositoryService _repositoryService;
    private readonly DomainModel _domainModel;

    public ClientController(RepositoryService repositoryService, DomainModel domainModel)
    {
        _repositoryService = repositoryService;
        _domainModel = domainModel;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var clients = await _repositoryService.GetAll<Client>(ApiUrls.ClientUrl);
        _domainModel.Clients = clients == null ? new List<Client>() : clients.ToList();
        return View(_domainModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Client client)
    {
        client.Id = 0;
        var createdClient = await _repositoryService.Create<Client>(ApiUrls.ClientUrl, client);
        if (createdClient == null)
        {
            ModelState.AddModelError("", "Failed to create the client.");
            return View(client);
        }

        return RedirectToAction("Details", new { id = createdClient.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var client = await _repositoryService.GetById<Client>(ApiUrls.ClientUrl, id);
        if (client == null)
        {
            return RedirectToAction("Index");
        }

        return View(client);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Client client)
    {
        var isUpdatedClient = await _repositoryService.Update(ApiUrls.ClientUrl, id, client);
        if (isUpdatedClient)
        {
            return RedirectToAction("Details", new { id });
        }

        ModelState.AddModelError("", "Failed to update the client.");
        return View(client);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var client = await _repositoryService.GetById<Client>(ApiUrls.ClientUrl, id);
        if (client == null)
        {
            return RedirectToAction("Index");
        }

        return View(client);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var client = await _repositoryService.GetById<Client>(ApiUrls.ClientUrl, id);
        if (client == null)
        {
            return RedirectToAction("Index");
        }

        return View(client);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, Client client)
    {
        await _repositoryService.Delete(ApiUrls.ClientUrl, id);
        return RedirectToAction("Index");
    }
}