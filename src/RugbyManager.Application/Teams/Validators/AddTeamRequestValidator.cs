using FluentValidation;
using RugbyManager.Application.Common.Models;
using RugbyManager.Application.Teams.Commands;

namespace RugbyManager.Application.Teams.Validators;

public class AddTeamRequestValidator : AbstractValidator<AddTeamRequest>
{
    public AddTeamRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}