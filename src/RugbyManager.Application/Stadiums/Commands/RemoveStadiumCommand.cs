using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Application.Common.Models.Stadium;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Stadiums.Commands;

public class RemoveStadiumCommand : IRequest, IMapFrom<RemoveStadiumRequest>
{
    public int StadiumId { get; set; }
}

public class RemoveStadiumCommandHandler : IRequestHandler<RemoveStadiumCommand>
{
    private readonly IAppDbContext _appDbContext;

    public RemoveStadiumCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Handle(RemoveStadiumCommand request, CancellationToken cancellationToken)
    {
        var stadium =
            await _appDbContext.Stadiums.FirstOrDefaultAsync(s => s.Id == request.StadiumId);

        if (stadium is null)
        {
            throw new StadiumNotFoundException(request.StadiumId);
        }

        var teamsLinkedToStadium = await _appDbContext
                                         .Teams.Where(t =>
                                             t.Stadium != null && t.Stadium.Id == stadium.Id)
                                         .AnyAsync();

        if (teamsLinkedToStadium)
        {
            throw new StadiumInUseException(stadium.Name);
        }

        _appDbContext.Stadiums.Remove(stadium);

        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}