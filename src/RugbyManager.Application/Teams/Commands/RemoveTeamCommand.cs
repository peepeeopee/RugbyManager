using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Application.Common.Models.Team;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Teams.Commands;

public class RemoveTeamCommand : IRequest, IMapFrom<RemoveTeamRequest>
{
    public int TeamId { get; set; }
}

public class RemoveTeamCommandHandler : IRequestHandler<RemoveTeamCommand>
{
    private readonly IAppDbContext _appDbContext;

    public RemoveTeamCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Handle(RemoveTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _appDbContext.Teams.FirstOrDefaultAsync(t => t.Id == request.TeamId, cancellationToken);

        if (team is null)
        {
            throw new TeamNotFoundException(request.TeamId);
        }

        _appDbContext.Teams.Remove(team);

        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}