using FluentValidation;
using RugbyManager.Application.Teams.Queries;

namespace RugbyManager.Application.Teams.Validators;

public class GetTeamQueryValidator : AbstractValidator<GetTeamByIdQuery>
{
    public GetTeamQueryValidator()
    {
        RuleFor(x => x.TeamId)
            .GreaterThan(0);
    }
}