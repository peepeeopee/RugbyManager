using FluentValidation;
using RugbyManager.Application.Common.Models;

namespace RugbyManager.Application.Common.Validators;

public class AddStadiumRequestValidator : AbstractValidator<AddStadiumRequest>
{
    public AddStadiumRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.Capacity)
            .GreaterThan(0);
    }
}