using FluentValidation;

namespace ProjectManagement.Application.Services.Project.CreateProject.Dtos;

public class CreateProjectRequestValidator:AbstractValidator<CreateProjectRequestDto>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(d => d.Name)
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");
        RuleFor(d => d.Description)
            .Length(3, 5000).WithMessage("Description must be between 3 and 5000 characters.");
    }
}