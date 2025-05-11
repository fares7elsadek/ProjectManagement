using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Repositories;
using ProjectManagement.Infrastructure.Data;

namespace ProjectManagement.Infrastructure.Repositories;

public class ApplicationUserRepository(AppDbContext db):Repository<ApplicationUser>(db),IApplicationUserRepository
{
    
}