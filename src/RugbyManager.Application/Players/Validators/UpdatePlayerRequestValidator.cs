using FluentValidation;
using RugbyManager.Application.Common.Models;

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
    }
}