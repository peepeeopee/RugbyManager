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
    private readonly IAppContext _appContext;
    private readonly IMapper _mapper;

    public AddPlayerCommandHandler(IAppContext appContext, IMapper mapper)
    {
        _appContext = appContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
    {
        var player = _mapper.Map<Player>(request);

        await _appContext.Players.AddAsync(player, cancellationToken);

        await _appContext.SaveChangesAsync(cancellationToken);

        return player.Id;
    }
}