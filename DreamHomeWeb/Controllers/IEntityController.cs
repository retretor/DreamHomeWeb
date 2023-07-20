using Microsoft.AspNetCore.Mvc;

namespace DreamHomeWeb.Controllers;

public interface IEntityController<in T> where T : class
{
    Task<IActionResult> Index();
    IActionResult Create();
    Task<IActionResult> Create(T entity);
    Task<IActionResult> Edit(int id);
    Task<IActionResult> Edit(int id, T entity);
    Task<IActionResult> Details(int id);
    Task<IActionResult> Delete(int id);
    Task<IActionResult> Delete(int id, T entity);
}