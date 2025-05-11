namespace ProjectManagement.Application.Services.Project.GetProjectWithTasks.Dtos;

public record TaskDto(
    string Title,
    string Description,
    string Status,
    string? UserId);