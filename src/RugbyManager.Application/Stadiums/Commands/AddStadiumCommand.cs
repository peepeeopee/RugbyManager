using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Interfaces;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Stadiums.Commands;

public class AddStadiumCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }

    public AddStadiumCommand(string name, int capacity)
    {
        Name = name;
        Capacity = capacity;
    }
}

public class AddStadiumCommandHandler : IRequestHandler<AddStadiumCommand, int>
{
    private readonly IAppContext _appContext;

    public AddStadiumCommandHandler(IAppContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<int> Handle(AddStadiumCommand request, CancellationToken cancellationToken)
    {
        if (await _appContext.Stadiums
                             .FirstOrDefaultAsync(t =>
                                     t.Name == request.Name,
                                 cancellationToken) is not null)
        {
            throw new StadiumAlreadyExistsException(request.Name);
        }

        Stadium stadium = new()
        {
            Name = request.Name,
            Capacity = request.Capacity
        };

        await _appContext.Stadiums.AddAsync(stadium, cancellationToken);

        await _appContext.SaveChangesAsync(cancellationToken);

        return stadium.Id;
    }
}