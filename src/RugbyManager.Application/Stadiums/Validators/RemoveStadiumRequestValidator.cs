using FluentValidation;
using RugbyManager.Application.Common.Models.Stadium;

namespace RugbyManager.Application.Stadiums.Validators;

public class RemoveStadiumRequestValidator : AbstractValidator<RemoveStadiumRequest>
{
    public RemoveStadiumRequestValidator()
    {
        RuleFor(x => x.StadiumId)
            .GreaterThan(0);
    }
}