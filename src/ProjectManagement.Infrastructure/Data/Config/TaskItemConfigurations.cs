using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Infrastructure.Data.Config;

public class TaskItemConfigurations:IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.Property(x => x.Id)
            .HasDefaultValueSql("newid()");
        
        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()");
        
        builder.Property(x => x.Status)
            .HasConversion(x => x.ToString(), x => (TaskItemStatus)Enum.Parse(typeof(TaskItemStatus), x));
    }
}