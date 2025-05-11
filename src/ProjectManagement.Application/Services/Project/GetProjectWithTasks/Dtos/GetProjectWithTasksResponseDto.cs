using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Services.Project.GetProjectWithTasks.Dtos;

public record GetProjectWithTasksResponseDto(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime? EndDate,
    List<TaskDto> Tasks);
