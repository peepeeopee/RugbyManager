using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Players.Commands;

public class UpdatePlayerCommand : IRequest
{
    public int PlayerId { get; set; }
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public double? Height { get; set; }
}

public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand>
{
    private readonly IAppDbContext _appDbContext;

    public UpdatePlayerCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player =
            await _appDbContext.Players.FirstOrDefaultAsync(p => p.Id == request.PlayerId,
                cancellationToken);

        if (player is null)
        {
            throw new PlayerNotFoundException(request.PlayerId);
        }

        player.FirstName = request.FirstName!;
        player.Surname = request.Surname!;
        player.Height = request.Height;

        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}