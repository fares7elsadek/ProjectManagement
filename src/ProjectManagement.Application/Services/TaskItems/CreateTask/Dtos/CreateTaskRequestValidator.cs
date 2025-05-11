using FluentValidation;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Application.Services.TaskItems.CreateTask.Dtos;

public class CreateTaskRequestValidator: AbstractValidator<CreateTaskRequestDto>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(d => d.Title)
            .Length(3, 50).WithMessage("Title must be between 3 and 50 characters.");
        RuleFor(d => d.Description)
            .Length(3, 5000).WithMessage("Description must be between 3 and 5000 characters.");
        RuleFor(d => d.Status)
            .Must(status => Enum.TryParse<TaskItemStatus>(status, true, out var parsedStatus)
                            && (parsedStatus == TaskItemStatus.PENDING 
                                || parsedStatus == TaskItemStatus.IN_PROGRESS 
                                || parsedStatus == TaskItemStatus.DONE))
            .WithMessage("Status must be one of the following: PENDING, IN_PROGRESS, or DONE.");

        RuleFor(x => x.ProjectId)
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("Id must be a valid GUID.");

    }
}