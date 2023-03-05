using FluentValidation;
using RugbyManager.Application.Common.Models;
using RugbyManager.Application.Players.Commands;

namespace RugbyManager.Application.Players.Validators;

public class AddPlayerRequestValidator : AbstractValidator<AddPlayerRequest>
{
    public AddPlayerRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.Surname)
            .NotEmpty();
        RuleFor(x => x.Height)
            .GreaterThan(0);
    }
}