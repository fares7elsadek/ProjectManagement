using ProjectManagement.Application.Services.TaskItems.GetTasks.Dtos;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Repositories;

namespace ProjectManagement.Application.Services.TaskItems.GetTasks;

public class GetTasksService(IUnitOfWork unitOfWork)
{
    public async Task<List<GetTasksResponseDto>> Handler(string? ProjectId , string? UserId)
    {
        IEnumerable<TaskItem> response = null;
        if (ProjectId != null && UserId != null)
        {
            response = await unitOfWork.TaskItems.GetAllWithConditionAsync(x => x.ProjectId == ProjectId &&
                x.AssignedUserId == UserId);

            return response.Select(x =>
                new GetTasksResponseDto(x.Id,x.Title, x.Description, x.Status.ToString(), x.ProjectId,x.AssignedUserId)).ToList();
        }else if (ProjectId != null)
        {
            response = await unitOfWork.TaskItems.GetAllWithConditionAsync(x => x.ProjectId == ProjectId);
            return response.Select(x =>
                new GetTasksResponseDto(x.Id,x.Title, x.Description, x.Status.ToString(), x.ProjectId,x.AssignedUserId)).ToList();
        }else if (UserId != null)
        {
            response = await unitOfWork.TaskItems.GetAllWithConditionAsync(x => x.AssignedUserId == UserId);
            return response.Select(x =>
                new GetTasksResponseDto(x.Id,x.Title, x.Description, x.Status.ToString(), x.ProjectId,x.AssignedUserId)).ToList();
        }
        else
        {
            response = await unitOfWork.TaskItems.GetAllAsync();
        }
        
        return response.Select(x =>
            new GetTasksResponseDto(x.Id,x.Title, x.Description, x.Status.ToString(), x.ProjectId,x.AssignedUserId)).ToList();
    }
}