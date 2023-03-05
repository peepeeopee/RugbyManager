using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Application.Common.Models;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Teams.Commands;

public class AddTeamCommand : IRequest<int>, IMapFrom<AddTeamRequest>
{
    public string Name { get; init; }
}

public class AddTeamCommandHandler : IRequestHandler<AddTeamCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public AddTeamCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddTeamCommand request, CancellationToken cancellationToken)
    {
        //if (await _appDbContext.Teams
        //                     .FirstOrDefaultAsync(t =>
        //                             t.Name == request.Name,
        //                         cancellationToken) is not null)
        //{
        //    throw new TeamAlreadyExistsException(request.Name);
        //}

        Team team = new()
        {
            Name = request.Name
        };

        await _appDbContext.Teams.AddAsync(team, cancellationToken);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return team.Id;
    }
}