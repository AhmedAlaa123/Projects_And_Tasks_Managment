using Microsoft.AspNetCore.Mvc;
using Ui.Services;
using Ui.Services.Dtos;

namespace Ui.Controllers;

public class ProjectController : Controller
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService) => _projectService = projectService;
   

 
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var result = await _projectService.GetAllAsync(pageNumber, pageSize);
            return View(result.Data);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View(new PagedResult<ProjectDto>());
        }
    }
 
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var project = await _projectService.GetByIdAsync(id);
            return View(project.Data);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }
 
    [HttpGet]
    public IActionResult Create() => View();
 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProjectDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        try
        {
            await _projectService.CreateAsync(dto);
            TempData["Success"] = "Project created successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(dto);
        }
    }

    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var project = await _projectService.GetByIdAsync(id);
            var dto = new UpdateProjectDto
            {
                Name = project!.Data!.Name,
                Description = project!.Data!.Description
            };
            ViewBag.ProjectId = id;
            return View(dto);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateProjectDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ProjectId = id;
            return View(dto);
        }

        try
        {
            await _projectService.UpdateAsync(id, dto);
            TempData["Success"] = "Project updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.ProjectId = id;
            return View(dto);
        }
    }

 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _projectService.DeleteAsync(id);
            TempData["Success"] = "Project deleted successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
