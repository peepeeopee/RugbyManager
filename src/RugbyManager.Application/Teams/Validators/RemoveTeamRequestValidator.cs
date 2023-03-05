using FluentValidation;
using RugbyManager.Application.Common.Models.Team;

namespace RugbyManager.Application.Teams.Validators;

public class RemoveTeamRequestValidator : AbstractValidator<RemoveTeamRequest>
{
    public RemoveTeamRequestValidator()
    {
        RuleFor(x => x.TeamId)
            .GreaterThan(0);
    }
}