using FluentValidation;
using RugbyManager.Application.Common.Models.Stadium;

namespace RugbyManager.Application.Stadiums.Validators;

public class UpdateStadiumRequestValidator : AbstractValidator<UpdateStadiumRequest>
{
    public UpdateStadiumRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.Capacity)
            .GreaterThan(0);
    }
}