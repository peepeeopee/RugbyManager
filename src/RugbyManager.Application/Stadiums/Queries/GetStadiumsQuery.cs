using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Stadiums.Queries;

public class GetStadiumsQuery : IRequest<List<StadiumDto>>
{
}

public class GetStadiumsQueryHandler : IRequestHandler<GetStadiumsQuery, List<StadiumDto>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetStadiumsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<List<StadiumDto>> Handle(
        GetStadiumsQuery request,
        CancellationToken cancellationToken) =>
        await _appDbContext.Stadiums
                           .ProjectTo<StadiumDto>(_mapper.ConfigurationProvider)
                           .ToListAsync(cancellationToken);
}

