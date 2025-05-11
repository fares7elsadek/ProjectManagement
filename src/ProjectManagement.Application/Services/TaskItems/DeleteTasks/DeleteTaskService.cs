using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Repositories;

namespace ProjectManagement.Application.Services.TaskItems.DeleteTasks;

public class DeleteTaskService(IUnitOfWork unitOfWork)
{
    public async Task Handler(string TaskId)
    {
        var task = await unitOfWork.TaskItems.GetOrDefalutAsync(x => x.Id == TaskId);
        if(task is null)
            throw new NotFoundException(nameof(task), TaskId);
        
        unitOfWork.TaskItems.Remove(task);
        await unitOfWork.SaveAsync();
    }
}