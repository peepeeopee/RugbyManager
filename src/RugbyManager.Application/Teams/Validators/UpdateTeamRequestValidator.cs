using FluentValidation;
using RugbyManager.Application.Common.Models.Team;

namespace RugbyManager.Application.Teams.Validators;

public class UpdateTeamRequestValidator : AbstractValidator<UpdateTeamRequest>
{
    public UpdateTeamRequestValidator()
    {
        RuleFor(x => x.TeamId)
            .GreaterThan(0);
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}