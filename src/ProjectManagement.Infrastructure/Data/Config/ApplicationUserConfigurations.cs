using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Infrastructure.Data.Config;

public class ApplicationUserConfigurations:IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasDefaultValueSql("newid()");
        
        builder.HasMany(x => x.Tasks)
            .WithOne(x => x.AssignedUser)
            .HasForeignKey(x => x.AssignedUserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}