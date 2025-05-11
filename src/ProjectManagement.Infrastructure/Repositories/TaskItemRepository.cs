using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Repositories;
using ProjectManagement.Infrastructure.Data;

namespace ProjectManagement.Infrastructure.Repositories;

public class TaskItemRepository(AppDbContext db):Repository<TaskItem>(db),ITaskItemsRepository
{
    
}