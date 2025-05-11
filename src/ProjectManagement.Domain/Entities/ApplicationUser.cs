using Microsoft.AspNetCore.Identity;

namespace ProjectManagement.Domain.Entities;

public class ApplicationUser:IdentityUser
{
    public ApplicationUser()
    {
        Tasks = new List<TaskItem>();
    }
    public List<TaskItem> Tasks { get; set; } 
}