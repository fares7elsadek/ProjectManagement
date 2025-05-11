using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Services.Auth.Jwt;
using ProjectManagement.Application.Services.Auth.Login;
using ProjectManagement.Application.Services.Auth.Register;
using ProjectManagement.Application.Services.Project.CreateProject;
using ProjectManagement.Application.Services.Project.DeleteProject;
using ProjectManagement.Application.Services.Project.GetAllProjects;
using ProjectManagement.Application.Services.Project.GetProjectWithTasks;
using ProjectManagement.Application.Services.Project.UpdateProject;
using ProjectManagement.Application.Services.TaskItems.AssignTask;
using ProjectManagement.Application.Services.TaskItems.CreateTask;
using ProjectManagement.Application.Services.TaskItems.DeleteTasks;
using ProjectManagement.Application.Services.TaskItems.GetTasks;
using ProjectManagement.Application.Services.TaskItems.UpdateTask;
using ProjectManagement.Application.Services.User;

namespace ProjectManagement.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                    .AddFluentValidationAutoValidation();
        services.AddScoped<LoginService>();
        services.AddScoped<RegisterService>();
        services.AddScoped<CreateProjectService>();
        services.AddScoped<UpdateProjectService>();
        services.AddScoped<GetAllProjectsService>();
        services.AddScoped<GetProjectWithTasksService>();
        services.AddScoped<DeleteProjectService>();
        services.AddScoped<AssignTaskService>();
        services.AddScoped<CreateTaskService>();
        services.AddScoped<DeleteTaskService>();
        services.AddScoped<UpdateTaskService>();
        services.AddScoped<GetTasksService>();
        services.AddScoped<GetUserService>();
    }
}