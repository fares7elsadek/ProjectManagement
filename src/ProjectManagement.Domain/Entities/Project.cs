namespace ProjectManagement.Domain.Entities;

public class Project
{
    public Project()
    {
        Tasks = new List<TaskItem>();
    }
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<TaskItem> Tasks { get; set; } 
}