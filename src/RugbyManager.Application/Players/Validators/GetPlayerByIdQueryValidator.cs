using FluentValidation;
using RugbyManager.Application.Players.Queries;

namespace RugbyManager.Application.Players.Validators;

public class GetPlayerByIdQueryValidator : AbstractValidator<GetPlayerByIdQuery>
{
    public GetPlayerByIdQueryValidator()
    {
        RuleFor(x => x.PlayerId)
            .NotEmpty()
            .GreaterThan(0);
    }
}