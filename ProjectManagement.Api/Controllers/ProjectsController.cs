using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Models;
using ProjectManagement.Core.UseCases;
using ProjectManagement.Core.Entities;

namespace ProjectManagement.Api.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(IProjectService projectService, ILogger<ProjectsController> logger)
    {
        _projectService = projectService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
    {
        // Input validation, a simple way
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("The name project is required");
        }

        // Business logic
        try
        {
            // Call to Core to create a new project
            var projectId = await _projectService.CreateProjectAsync(request.Name);

            // Return a 201 responce: Create project with the URI of the new project
            return CreatedAtAction(nameof(GetProjectById), new { id = projectId }, new { id = projectId });
            // return StatusCode(201, new { id = projectId });
        }
        catch (ArgumentException ex)
        {
            // Handle the argument exception throw by the Core
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{projectId}/tasks")]
    public async Task<IActionResult> AddTaskToProject(Guid projectId, [FromBody] AddTaskRequest request)
    {
        // Business logic
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("The title of the task is required");
        }

        try
        {
            await _projectService.AddTaskToProjectAsync(projectId, request.Title);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid Task data");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado al añadir una tarea al proyecto {projectId}.", projectId);
            return StatusCode(500, "Unexpected Error, internal server Error.");
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Project>>> GetAllProjects()
    {
        // Business logic
        var projects = await _projectService.GetAllProjectsAsync();
        return Ok(projects);
    }

    // // This metod is a position marked to CreatedAtAction root
    // [HttpGet("{id}")]
    // // public async Task<IActionResult> GetProjectById (Guid id)
    // public async Task<IActionResult> GetProjectById(Guid id)
    // {
    //     // placehorder for now
    //     return Ok(new { id = id, message = "placehorder GetProjectById" });
    // }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(Guid id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);

        if (project == null)
        {
            return NotFound($"Project with id {id} not found.");
        }

        return Ok(project);
    }

    [HttpPut("{projectId}/tasks/{taskId}/complete")]
    public async Task<IActionResult> MarkTaskAsCompleted(Guid projectId, Guid taskId)
    {
        try
        {
            await _projectService.MarkTaskAsCompletedAsync(projectId, taskId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Happend an error while mark as completed a task {taskId}");
            return StatusCode(500, "Unexpected Error, internal server Error.");
        }
    }

}