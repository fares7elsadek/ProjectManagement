using ProjectManagement.Application.Services.Project.CreateProject.Dtos;
using ProjectManagement.Domain.Repositories;

namespace ProjectManagement.Application.Services.Project.CreateProject;

public class CreateProjectService(IUnitOfWork unitOfWork)
{
    public async Task Handler(CreateProjectRequestDto createProjectRequestDto)
    {
        await unitOfWork.Project.AddAsync(new Domain.Entities.Project()
        {
            Name = createProjectRequestDto.Name,
            Description = createProjectRequestDto.Description,
        });
        await unitOfWork.SaveAsync();
    }
}