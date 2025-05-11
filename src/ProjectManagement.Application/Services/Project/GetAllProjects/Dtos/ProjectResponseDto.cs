using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Services.Project.GetAllProjects.Dtos;

public record ProjectResponseDto(string Id,string Name,string Description,DateTime StartDate,DateTime? EndDate);
