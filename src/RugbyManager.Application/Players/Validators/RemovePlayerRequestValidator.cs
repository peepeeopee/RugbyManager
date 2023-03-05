using FluentValidation;
using RugbyManager.Application.Common.Models.Player;

namespace RugbyManager.Application.Players.Validators;

public class RemovePlayerRequestValidator : AbstractValidator<RemovePlayerRequest>
{
    public RemovePlayerRequestValidator() =>
        RuleFor(x => x.PlayerId)
            .GreaterThan(0);
}