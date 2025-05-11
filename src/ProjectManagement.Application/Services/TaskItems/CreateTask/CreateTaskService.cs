using ProjectManagement.Application.Services.TaskItems.CreateTask.Dtos;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Repositories;

namespace ProjectManagement.Application.Services.TaskItems.CreateTask;

public class CreateTaskService(IUnitOfWork unitOfWork)
{
    public async Task Handler(CreateTaskRequestDto request)
    {
        var project = await unitOfWork.Project.GetOrDefalutAsync(x => x.Id == request.ProjectId);
        if(project == null)
            throw new NotFoundException(nameof(Project), request.ProjectId);
        
        unitOfWork.TaskItems.AddAsync(new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            ProjectId = request.ProjectId,
            Status = Enum.Parse<TaskItemStatus>(request.Status, ignoreCase: true),
            AssignedUserId =  string.IsNullOrEmpty(request.UserId) ? null : request.UserId,
            
        });
        await unitOfWork.SaveAsync();
    }
}