using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Application.Common.Models.Transfers;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Transfers.Commands;

public class TransferPlayerCommand : IRequest, IMapFrom<TransferPlayerRequest>
{
    public int PlayerId { get; set; }
    public int TeamId { get; set; }
}

public class TransferPlayerCommandHandler : IRequestHandler<TransferPlayerCommand>
{
    private readonly IAppDbContext _appDbContext;

    public TransferPlayerCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Handle(TransferPlayerCommand request, CancellationToken cancellationToken)
    {
        var player =
            await _appDbContext.Players.FirstOrDefaultAsync(p => p.Id == request.PlayerId,
                cancellationToken);

        if (player is null)
        {
            throw new PlayerNotFoundException(request.PlayerId);
        }

        var newTeam =
            await _appDbContext.Teams.FirstOrDefaultAsync(t => t.Id == request.TeamId,
                cancellationToken);

        if (newTeam is null)
        {
            throw new TeamNotFoundException(request.TeamId);
        }

        var memberships = await _appDbContext
                                .TeamMemberships
                                .Where(tm => tm.PlayerId == request.PlayerId)
                                .ToListAsync(cancellationToken);

        var transfers = memberships.Any() 
                ? memberships.Select(m => new Transfer()
            {
                FromTeamId = m.TeamId,
                PlayerId = m.PlayerId,
                ToTeamId = newTeam.Id
            })
                : new List<Transfer>()
                {
                    new()
                    {
                        PlayerId = player.Id,
                        ToTeamId = newTeam.Id
                    }
                };

        
        _appDbContext.TeamMemberships.RemoveRange(memberships);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        TeamMembership newMembership = new()
        {
            PlayerId = request.PlayerId,
            TeamId = request.TeamId,
        };

        await _appDbContext.TeamMemberships.AddAsync(newMembership, cancellationToken);


        await _appDbContext.Transfers.AddRangeAsync(transfers, cancellationToken);

        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}