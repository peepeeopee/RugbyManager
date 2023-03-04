using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Teams.Commands;

public class AddTeamCommand : IRequest<int>
{
    public string Name { get; private set; }

    public AddTeamCommand(string name)
    {
        Name = name;
    }
}

public class AddTeamCommandHandler : IRequestHandler<AddTeamCommand, int>
{
    private readonly IAppContext _appContext;
    private readonly IMapper _mapper;

    public AddTeamCommandHandler(IAppContext appContext, IMapper mapper)
    {
        _appContext = appContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddTeamCommand request, CancellationToken cancellationToken)
    {
        if (await _appContext.Teams
                             .FirstOrDefaultAsync(t =>
                                     t.Name == request.Name,
                                 cancellationToken) is not null)
        {
            throw new TeamAlreadyExistsException(request.Name);
        }

        var team = _mapper.Map<Team>(request);

        await _appContext.Teams.AddAsync(team, cancellationToken);

        await _appContext.SaveChangesAsync(cancellationToken);

        return team.Id;
    }
}