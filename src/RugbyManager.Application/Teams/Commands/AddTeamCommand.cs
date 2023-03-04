using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Interfaces;
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

    public AddTeamCommandHandler(IAppContext appContext)
    {
        _appContext = appContext;
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

        Team newTeam = new()
        {
            Name = request.Name
        };

        await _appContext.Teams.AddAsync(newTeam, cancellationToken);

        await _appContext.SaveChangesAsync(cancellationToken);

        return newTeam.Id;
    }
}