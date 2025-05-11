using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Project.CreateProject;
using ProjectManagement.Application.Services.Project.CreateProject.Dtos;
using ProjectManagement.Application.Services.Project.DeleteProject;
using ProjectManagement.Application.Services.Project.GetAllProjects;
using ProjectManagement.Application.Services.Project.GetProjectWithTasks;
using ProjectManagement.Application.Services.Project.UpdateProject;
using ProjectManagement.Application.Services.Project.UpdateProject.Dtos;
using ProjectManagement.Domain.Helpers;

namespace ProjectManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController: ControllerBase
{
    private ApiResponse apiResponse;
    private readonly CreateProjectService _createProjectService;
    private readonly UpdateProjectService _updateProjectService;
    private readonly DeleteProjectService _deleteProjectService;
    private readonly GetProjectWithTasksService _getProjectWithTasksService;
    private readonly GetAllProjectsService _getAllProjectsService;
    public ProjectController(UpdateProjectService updateProjectService
        , CreateProjectService createProjectService
        , DeleteProjectService deleteProjectService
        , GetProjectWithTasksService getProjectWithTasksService
        , GetAllProjectsService getAllProjectsService)
    {
        _updateProjectService = updateProjectService;
        _createProjectService = createProjectService;
        _deleteProjectService = deleteProjectService;
        _getProjectWithTasksService = getProjectWithTasksService;
        _getAllProjectsService = getAllProjectsService;
        apiResponse = new ApiResponse();
    }
    
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateProject([FromBody] CreateProjectRequestDto command)
    {
        await _createProjectService.Handler(command);

        apiResponse.IsSuccess = true;
        apiResponse.Errors = null;
        apiResponse.StatusCode = HttpStatusCode.OK;
        apiResponse.Result = "Project created successfully";
        return Ok(apiResponse);
    }
    
    [HttpPut("{projectId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateProject(string projectId,[FromBody] UpdateProjectDto command)
    {
        await _updateProjectService.Handler(projectId,command);

        apiResponse.IsSuccess = true;
        apiResponse.Errors = null;
        apiResponse.StatusCode = HttpStatusCode.OK;
        apiResponse.Result = "Project updated successfully";
        return Ok(apiResponse);
    }
    
    [HttpDelete("{projectId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteProject(string projectId)
    {
        await _deleteProjectService.Handler(projectId);

        apiResponse.IsSuccess = true;
        apiResponse.Errors = null;
        apiResponse.StatusCode = HttpStatusCode.OK;
        apiResponse.Result = "Project deleted successfully";
        return Ok(apiResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAllProjects()
    {
        var response = await _getAllProjectsService.Handler();

        apiResponse.IsSuccess = true;
        apiResponse.Errors = null;
        apiResponse.StatusCode = HttpStatusCode.OK;
        apiResponse.Result = response;
        return Ok(apiResponse);
    }
    
    
    [HttpGet("{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetProjectWithTasks(string projectId)
    {
        var response = await _getProjectWithTasksService.Handler(projectId);
        apiResponse.IsSuccess = true;
        apiResponse.Errors = null;
        apiResponse.StatusCode = HttpStatusCode.OK;
        apiResponse.Result = response;
        return Ok(apiResponse);
    }
}