using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Application.Common.Models.Team;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Teams.Commands;

public class UpdateTeamCommand : IRequest, IMapFrom<UpdateTeamRequest>
{
    public int TeamId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? StadiumId { get; set; }

}

public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand>
{
    private readonly IAppDbContext _appDbContext;

    public UpdateTeamCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _appDbContext.Teams.FirstOrDefaultAsync(t => t.Id == request.TeamId, cancellationToken);

        if (team is null)
        {
            throw new TeamNotFoundException(request.TeamId);
        }

        var stadium =
            await _appDbContext.Stadiums.FirstOrDefaultAsync(s => s.Id == request.StadiumId,cancellationToken);

        team.Name = request.Name;
        team.Stadium = stadium;

        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}