using AutoMapper;
using MediatR;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.Players.Commands;

public class AddPlayerCommand : IRequest<int>
{
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public double? Height { get; set; }
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
        var player = _mapper.Map<Player>(request);

        await _appDbContext.Players.AddAsync(player, cancellationToken);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return player.Id;
    }
}