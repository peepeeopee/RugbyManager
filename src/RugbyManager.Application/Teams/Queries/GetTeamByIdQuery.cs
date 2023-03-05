using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Teams.Queries;

public class GetTeamByIdQuery : IRequest<TeamDto>
{
    public int TeamId { get; set; }
}

public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, TeamDto>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetTeamByIdQueryHandler(
        IAppDbContext appDbContext,
        IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<TeamDto> Handle(
        GetTeamByIdQuery request,
        CancellationToken cancellationToken)
    {
        var team =
            await _appDbContext.Teams.FirstOrDefaultAsync(t => t.Id == request.TeamId, cancellationToken);

        if (team is null)
        {
            throw new TeamNotFoundException(request.TeamId);
        }

        return _mapper.Map<TeamDto>(team);
    }
}