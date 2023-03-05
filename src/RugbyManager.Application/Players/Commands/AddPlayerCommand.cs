using AutoMapper;
using MediatR;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Application.Common.Models.Player;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.Players.Commands;

public class AddPlayerCommand : IRequest<int>, IMapFrom<AddPlayerRequest>
{
    public string? FirstName { get; init; }
    public string? Surname { get; init; }
    public double? Height { get; init; }
}

public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public AddPlayerCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
    {
        Player player = new()
        {
            FirstName = request.FirstName!,
            Surname = request.Surname!,
            Height = request.Height
        };

        await _appDbContext.Players.AddAsync(player, cancellationToken);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return player.Id;
    }
}