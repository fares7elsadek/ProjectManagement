using FluentValidation;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Application.Services.TaskItems.UpdateTask.Dtos;

public class UpdateTaskRequestValidator:AbstractValidator<UpdateTaskReqeustDto>
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(d => d.status)
            .Must(status => Enum.TryParse<TaskItemStatus>(status, true, out var parsedStatus)
                            && (parsedStatus == TaskItemStatus.PENDING 
                                || parsedStatus == TaskItemStatus.IN_PROGRESS 
                                || parsedStatus == TaskItemStatus.DONE))
            .WithMessage("Status must be one of the following: PENDING, IN_PROGRESS, or DONE.");
    }
}