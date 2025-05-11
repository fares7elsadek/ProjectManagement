using FluentValidation;

namespace ProjectManagement.Application.Services.TaskItems.AssignTask.Dtos;

public class AssignTaskRequestValidator:AbstractValidator<AssignTaskRequestDto>
{
    public AssignTaskRequestValidator()
    {
        RuleFor(x => x.userId)
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("Id must be a valid GUID.");
    }
}