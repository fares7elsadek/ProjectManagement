using ProjectManagement.Application.Services.Project.UpdateProject.Dtos;
using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Repositories;

namespace ProjectManagement.Application.Services.Project.UpdateProject;

public class UpdateProjectService(IUnitOfWork unitOfWork)
{
    public async Task Handler(string projectId, UpdateProjectDto updateProjectDto)
    {
        var project = await unitOfWork.Project.GetOrDefalutAsync(x => x.Id == projectId);
        if(project == null)
            throw new NotFoundException(nameof(Project), projectId);
        project.Description = updateProjectDto.Description;
        project.Name = updateProjectDto.Name;
        await unitOfWork.SaveAsync();
    }
}