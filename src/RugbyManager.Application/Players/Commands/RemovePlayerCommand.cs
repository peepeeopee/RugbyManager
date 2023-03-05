using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Players.Commands;

public class RemovePlayerCommand : IRequest
{
    public int PlayerId { get; init; }
}

public class DeletePlayerCommandHandler : IRequestHandler<RemovePlayerCommand>
{
    private readonly IAppDbContext _appDbContext;

    public DeletePlayerCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Handle(RemovePlayerCommand request, CancellationToken cancellationToken)
    {
        var player =
            await _appDbContext.Players.FirstOrDefaultAsync(p => p.Id == request.PlayerId,
                cancellationToken);

        if (player is null)
        {
            throw new PlayerNotFoundException(request.PlayerId);
        }

        _appDbContext.Players.Remove(player);

        await _appDbContext.SaveChangesAsync(cancellationToken);

    }
}