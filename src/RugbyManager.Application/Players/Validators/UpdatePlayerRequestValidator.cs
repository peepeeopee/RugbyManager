using FluentValidation;
using RugbyManager.Application.Common.Models.Player;

namespace RugbyManager.Application.Players.Validators;

public class UpdatePlayerRequestValidator : AbstractValidator<UpdatePlayerRequest>
{
    public UpdatePlayerRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.Surname)
            .NotEmpty();
        RuleFor(x => x.Height)
            .GreaterThan(0);
        RuleFor(x => x.PlayerId)
            .GreaterThan(0);
    }
}