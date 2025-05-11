using ProjectManagement.Application.Services.Project.GetAllProjects.Dtos;
using ProjectManagement.Application.Services.Project.GetProjectWithTasks.Dtos;
using ProjectManagement.Domain.Repositories;

namespace ProjectManagement.Application.Services.Project.GetProjectWithTasks;

public class GetProjectWithTasksService(IUnitOfWork unitOfWork)
{
    public async Task<GetProjectWithTasksResponseDto> Handler(string projectId)
    {
        var response = await unitOfWork.Project.GetOrDefalutAsync(x => x.Id == projectId,
            IncludeProperties: "Tasks");
        
        List<TaskDto> tasks = new List<TaskDto>();
        foreach (var task in response.Tasks)
        {
            tasks.Add(new TaskDto(task.Title, task.Description, task.Status.ToString() ,task.AssignedUserId));
        }
        
        return new GetProjectWithTasksResponseDto(response.Name,response.Description
            ,response.StartDate,response.EndDate,tasks);
    }
}