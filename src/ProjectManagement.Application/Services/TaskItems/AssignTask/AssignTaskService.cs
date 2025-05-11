using ProjectManagement.Application.Services.TaskItems.AssignTask.Dtos;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Repositories;

namespace ProjectManagement.Application.Services.TaskItems.AssignTask;

public class AssignTaskService(IUnitOfWork unitOfWork)
{
    public async Task Handler(string taskId, AssignTaskRequestDto request)
    {
        var task = await unitOfWork.TaskItems.GetOrDefalutAsync(x => x.Id == taskId);
        if(task is null)
            throw new NotFoundException(nameof(task), taskId);

        if (request.assign)
        {
            if (!string.IsNullOrEmpty(task.AssignedUserId) &&
                task.AssignedUserId == request.userId)
            {
                throw new Exception("User already assigned to task");
            }
            
            task.AssignedUserId = request.userId;
        }
        else
        {
            if (!(!string.IsNullOrEmpty(task.AssignedUserId) &&
                task.AssignedUserId == request.userId))
            {
                throw new Exception("User is not assigned to this task");
            }
            task.AssignedUserId = null;
        }

        await unitOfWork.SaveAsync();
    }
}