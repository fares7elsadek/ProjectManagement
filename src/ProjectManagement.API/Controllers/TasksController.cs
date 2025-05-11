using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.TaskItems.AssignTask;
using ProjectManagement.Application.Services.TaskItems.AssignTask.Dtos;
using ProjectManagement.Application.Services.TaskItems.CreateTask;
using ProjectManagement.Application.Services.TaskItems.CreateTask.Dtos;
using ProjectManagement.Application.Services.TaskItems.DeleteTasks;
using ProjectManagement.Application.Services.TaskItems.GetTasks;
using ProjectManagement.Application.Services.TaskItems.UpdateTask;
using ProjectManagement.Application.Services.TaskItems.UpdateTask.Dtos;
using ProjectManagement.Domain.Helpers;

namespace ProjectManagement.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private ApiResponse apiResponse;
    private readonly CreateTaskService _createTaskService;
    private readonly UpdateTaskService _updateTaskService;
    private readonly DeleteTaskService _deleteTaskService;
    private readonly GetTasksService _getTasksService;
    private readonly AssignTaskService _assignTaskService;
    public TasksController(CreateTaskService createTaskService
        , UpdateTaskService updateTaskService
        , DeleteTaskService deleteTaskService
        , GetTasksService getTasksService
        , AssignTaskService assignTaskService)
    {
        _createTaskService = createTaskService;
        _updateTaskService = updateTaskService;
        _deleteTaskService = deleteTaskService;
        _getTasksService = getTasksService;
        _assignTaskService = assignTaskService;

        apiResponse = new ApiResponse();
    }
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateTask([FromBody] CreateTaskRequestDto command)
    {
        await _createTaskService.Handler(command);

        apiResponse.IsSuccess = true;
        apiResponse.Errors = null;
        apiResponse.StatusCode = HttpStatusCode.OK;
        apiResponse.Result = "Task created successfully";
        return Ok(apiResponse);
    }
    
    [HttpPut("{taskId}/status")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateTask(string taskId,UpdateTaskReqeustDto command)
    {
        await _updateTaskService.UpdateTask(taskId,command);

        apiResponse.IsSuccess = true;
        apiResponse.Errors = null;
        apiResponse.StatusCode = HttpStatusCode.OK;
        apiResponse.Result = "Task updated successfully";
        return Ok(apiResponse);
    }
    
    [HttpDelete("{taskId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteTask(string taskId)
    {
        await _deleteTaskService.Handler(taskId);

        apiResponse.IsSuccess = true;
        apiResponse.Errors = null;
        apiResponse.StatusCode = HttpStatusCode.OK;
        apiResponse.Result = "Task deleted successfully";
        return Ok(apiResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> FilterTask([FromQuery] string? projectId = null,[FromQuery] string? userId = null)
    {
        var response = await _getTasksService.Handler(projectId, userId);
        apiResponse.IsSuccess = true;
        apiResponse.Errors = null;
        apiResponse.StatusCode = HttpStatusCode.OK;
        apiResponse.Result = response;
        return Ok(apiResponse);
    }
    
    
    [HttpPut("{taskId}/assign")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AssignTask(string taskId,[FromBody] AssignTaskRequestDto command)
    {
        await _assignTaskService.Handler(taskId,command);
        apiResponse.IsSuccess = true;
        apiResponse.Errors = null;
        apiResponse.StatusCode = HttpStatusCode.OK;
        apiResponse.Result = "Task assigned successfully";
        return Ok(apiResponse);
    }
}