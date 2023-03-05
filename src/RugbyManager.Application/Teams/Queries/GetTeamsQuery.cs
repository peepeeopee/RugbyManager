using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Teams.Queries;

public class GetTeamsQuery : IRequest<List<TeamDto>>
{
}

public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, List<TeamDto>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetTeamsQueryHandler(
        IAppDbContext appDbContext,
        IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<List<TeamDto>> Handle(
        GetTeamsQuery request,
        CancellationToken cancellationToken) =>
        await _appDbContext.Teams
                           .OrderBy(t => t.Name)
                           .ProjectTo<TeamDto>(_mapper.ConfigurationProvider)
                           .ToListAsync(cancellationToken);
}

