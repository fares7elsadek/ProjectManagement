using ProjectManagement.Application.Services.Project.GetAllProjects.Dtos;
using ProjectManagement.Domain.Repositories;

namespace ProjectManagement.Application.Services.Project.GetAllProjects;

public class GetAllProjectsService(IUnitOfWork unitOfWork)
{
    public async Task<List<ProjectResponseDto>> Handler()
    {
        var response = await unitOfWork.Project.GetAllAsync();
        return response.Select(p 
            => new ProjectResponseDto(p.Id,p.Name,p.Description,p.StartDate,p.EndDate)).ToList();
    }
}