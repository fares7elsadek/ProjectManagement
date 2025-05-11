using Microsoft.EntityFrameworkCore.Infrastructure;
using ProjectManagement.Application.Services.TaskItems.UpdateTask.Dtos;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Enums;
using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Repositories;

namespace ProjectManagement.Application.Services.TaskItems.UpdateTask;

public class UpdateTaskService(IUnitOfWork unitOfWork)
{
    public async Task UpdateTask(string taskId,UpdateTaskReqeustDto request)
    {
        var task = await unitOfWork.TaskItems.GetOrDefalutAsync(x => x.Id == taskId);
        if(task == null)
            throw new NotFoundException(nameof(task), taskId);
        
        task.Status= Enum.Parse<TaskItemStatus>(request.status, true);
        await unitOfWork.SaveAsync();
    }
}