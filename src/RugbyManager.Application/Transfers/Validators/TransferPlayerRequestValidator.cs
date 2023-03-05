using FluentValidation;
using RugbyManager.Application.Common.Models.Transfers;

namespace RugbyManager.Application.Transfers.Validators;

public class TransferPlayerRequestValidator : AbstractValidator<TransferPlayerRequest>
{
    public TransferPlayerRequestValidator()
    {
        RuleFor(x => x.TeamId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.PlayerId)
            .NotEmpty()
            .GreaterThan(0);
    }
}