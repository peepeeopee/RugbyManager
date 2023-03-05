using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;

namespace RugbyManager.Application.Players.Queries;

public class GetPlayersQuery : IRequest<List<PlayerDto>>
{
}

public class GetPlayersQueryHandler : IRequestHandler<GetPlayersQuery, List<PlayerDto>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetPlayersQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<List<PlayerDto>> Handle(
        GetPlayersQuery request,
        CancellationToken cancellationToken) =>
        await _appDbContext.Players
                           .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
                           .ToListAsync(cancellationToken);
}