using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Helpers;
using ProjectManagement.Infrastructure.Data;

namespace ProjectManagement.Infrastructure.Seeder;

public class AppAppSeeder(AppDbContext db):IAppSeeder
{
    public async Task SeedAsync()
    {
        if (db.Database.GetPendingMigrations().Any())
        {
            await db.Database.MigrateAsync();
        }

        if (await db.Database.CanConnectAsync())
        {
            if (!db.Roles.Any())
            {
                db.Roles.AddRange(GetRoles);
                await db.SaveChangesAsync();
            }
        }
    }

    public IEnumerable<IdentityRole> GetRoles =>
    [
        new IdentityRole { Name = UserRoles.ADMIN, NormalizedName = UserRoles.ADMIN.ToUpper() },
        new IdentityRole { Name = UserRoles.USER, NormalizedName = UserRoles.USER.ToUpper() }
    ];
}