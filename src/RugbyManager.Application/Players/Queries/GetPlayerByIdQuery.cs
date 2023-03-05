using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Players.Queries;


public class GetPlayerByIdQuery : IRequest<PlayerDto>
{
    public int PlayerId { get; init; }
}

public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, PlayerDto>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetPlayerByIdQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<PlayerDto> Handle(
        GetPlayerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var player = await _appDbContext.Players.FirstOrDefaultAsync(p => p.Id == request.PlayerId);

        if (player is null)
        {
            throw new PlayerNotFoundException(request.PlayerId);
        }

        return _mapper.Map<PlayerDto>(player);
    }
}