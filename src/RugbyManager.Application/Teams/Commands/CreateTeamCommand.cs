using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Interfaces;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Teams.Commands;

public class CreateTeamCommand : IRequest<int>
{
    public string Name { get; private set; }

    public CreateTeamCommand(string name)
    {
        Name = name;
    }
}

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, int>
{
    private readonly IAppContext _appContext;

    public CreateTeamCommandHandler(IAppContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<int> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        if (await _appContext.Teams
                             .FirstOrDefaultAsync(t =>
                                     t.Name == request.Name,
                                 cancellationToken) is not null)
        {
            throw new TeamAlreadyExistsException(request.Name);
        }

        Team newTeam = new()
        {
            Name = request.Name
        };

        await _appContext.Teams.AddAsync(newTeam, cancellationToken);

        await _appContext.SaveChangesAsync(cancellationToken);

        return newTeam.Id;
    }
}