using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Domain.Entities;

public class TaskItem
{
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public TaskItemStatus Status { get; set; } 
    public DateTime CreatedAt { get; set; }
    public string ProjectId { get; set; } = default!;
    public Project Project { get; set; } = default!;
    public string? AssignedUserId { get; set; }
    public ApplicationUser AssignedUser { get; set; } = default!;
}