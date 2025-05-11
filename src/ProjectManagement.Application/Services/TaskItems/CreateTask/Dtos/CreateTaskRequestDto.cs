namespace ProjectManagement.Application.Services.TaskItems.CreateTask.Dtos;

public record CreateTaskRequestDto(
    string Title,
    string Description,
    string Status,
    string ProjectId,
    string? UserId);
