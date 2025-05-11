namespace ProjectManagement.Domain.Repositories;

public interface IUnitOfWork
{
    IApplicationUserRepository User { get; }
    IProjectRepository Project { get; }
    ITaskItemsRepository TaskItems { get; }
    Task SaveAsync();
}