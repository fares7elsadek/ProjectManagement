using ProjectManagement.Domain.Repositories;
using ProjectManagement.Infrastructure.Data;

namespace ProjectManagement.Infrastructure.Repositories;

public class UnitOfWork:IUnitOfWork
{
    public IApplicationUserRepository User { get; }
    public IProjectRepository Project { get; }
    public ITaskItemsRepository TaskItems { get; }
    private readonly AppDbContext _db;
    public UnitOfWork(AppDbContext db)
    {
        _db = db;
        User = new ApplicationUserRepository(_db);
        Project = new ProjectRepository(_db);
        TaskItems = new TaskItemRepository(_db);
    }
    
    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}