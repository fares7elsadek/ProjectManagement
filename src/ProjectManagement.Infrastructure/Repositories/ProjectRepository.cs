using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Repositories;
using ProjectManagement.Infrastructure.Data;

namespace ProjectManagement.Infrastructure.Repositories;

public class ProjectRepository(AppDbContext db):Repository<Project>(db),IProjectRepository
{
    
}