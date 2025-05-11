namespace ProjectManagement.Application.Services.TaskItems.GetTasks.Dtos;

public record GetTasksResponseDto(string id,
    string Title,
    string Description,
    string Status,
    string ProjectId,
    string? UserId);