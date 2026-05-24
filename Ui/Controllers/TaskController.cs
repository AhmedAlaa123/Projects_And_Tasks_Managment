using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Ui.Services;
using Ui.Services.Dtos;

namespace Ui.Controllers;

public class TaskController : Controller
{
    private readonly ITaskService _taskService;
    private readonly IProjectService _projectService;

    public TaskController(ITaskService taskService, IProjectService projectService)
    {
        _taskService = taskService;
        _projectService = projectService;
    }



    [HttpGet]
    public IActionResult Create(int projectId) {

        ViewBag.ProjectId = projectId;
        return View(); }

 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        if (!ModelState.IsValid)
        { return View(dto); }
        try
        {
            await _taskService.CreateAsync(dto);
            TempData["Success"] = "Task created successfully";
            return RedirectToAction(nameof(Index), new { dto.ProjectId });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(dto);
        }
    }

 
    [HttpGet]
    public IActionResult Edit(int id, string title, string description,
                              Domain.Enums.TaskStatus status, TaskPriority priority, DateTime dueDate,int projectId)
    {
        var dto = new UpdateTaskDto
        {
            Title = title,
            Description = description,
            Status = status,
            Priority = priority,
            DueDate = dueDate,
            ProjectId= projectId,
            Id=id
        };
        ViewBag.TaskId = id;
        return View(dto);
    }

 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateTaskDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.TaskId = id;

            return View(dto);
        }
        try
        {
            dto.Id = id;
            await _taskService.UpdateAsync(id, dto);
            TempData["Success"] = "Task updated successfully";
            return RedirectToAction(nameof(Index), new { dto.ProjectId });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.TaskId = id;
            return View(dto);
        }
    }

    //// ✅ Update Status
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> UpdateStatus(int id, TaskStatus status)
    //{
    //    try
    //    {
    //        await _taskService.UpdateStatusAsync(id, new UpdateTaskStatusDto { Status = status });
    //        TempData["Success"] = "Task status updated successfully";
    //    }
    //    catch (Exception ex)
    //    {
    //        TempData["Error"] = ex.Message;
    //    }
    //    return RedirectToAction(nameof(Index));
    //}

    // ✅ Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _taskService.DeleteAsync(id);
            TempData["Success"] = "Task deleted successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Index(int projectId)
    {
        try
        {
            var result = await _projectService.GetProjectTasks(projectId);
            ViewBag.ProjectId = projectId;
            return View(result.Data);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View(new PagedResult<TaskDto>());
        }
    }
    // ✅ Update Status POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, int projectId, int status)
    {
        try
        {
            await _taskService.UpdateStatusAsync(id, new TaskUpdateStatusDto
            {
                Status = (Domain.Enums.TaskStatus)status,
                Id = id
            });
            TempData["Success"] = "Task status updated successfully";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index), new { projectId });
    }
}
