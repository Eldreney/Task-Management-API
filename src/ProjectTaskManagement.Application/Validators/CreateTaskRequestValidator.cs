using FluentValidation;
using ProjectTaskManagement.Application.DTOs.Tasks;

namespace ProjectTaskManagement.Application.Validators;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(1000);
        RuleFor(x => x.Priority).IsInEnum();
    }
}
