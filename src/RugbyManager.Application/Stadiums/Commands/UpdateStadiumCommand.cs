using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Application.Common.Models.Stadium;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Stadiums.Commands;

public class UpdateStadiumCommand : IRequest, IMapFrom<UpdateStadiumRequest>
{
    public int StadiumId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Capacity { get; set; }
}

public class UpdateStadiumCommandHandler : IRequestHandler<UpdateStadiumCommand>
{
    private readonly IAppDbContext _appDbContext;

    public UpdateStadiumCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Handle(UpdateStadiumCommand request, CancellationToken cancellationToken)
    {
        var stadium =
            await _appDbContext.Stadiums.FirstOrDefaultAsync(s => s.Id == request.StadiumId, cancellationToken);

        if (stadium is null)
        {
            throw new StadiumNotFoundException(request.StadiumId);
        }

        stadium.Name = request.Name;
        stadium.Capacity = request.Capacity;

        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}