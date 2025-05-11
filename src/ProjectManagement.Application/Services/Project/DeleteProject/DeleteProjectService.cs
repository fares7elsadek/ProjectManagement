using ProjectManagement.Application.Services.Project.CreateProject.Dtos;
using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Repositories;

namespace ProjectManagement.Application.Services.Project.DeleteProject;

public class DeleteProjectService(IUnitOfWork unitOfWork)
{
    public async Task Handler(string projectId)
    {
        var project = await unitOfWork.Project.GetOrDefalutAsync(x => x.Id == projectId);
        if(project is null)
            throw new NotFoundException(nameof(project), projectId);
        unitOfWork.Project.Remove(project);
        await unitOfWork.SaveAsync();
    }
}