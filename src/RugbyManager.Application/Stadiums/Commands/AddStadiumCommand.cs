using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
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
    private readonly IMapper _mapper;

    public AddStadiumCommandHandler(IAppContext appContext, IMapper mapper)
    {
        _appContext = appContext;
        _mapper = mapper;
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

        var stadium = _mapper.Map<Stadium>(request);

        await _appContext.Stadiums.AddAsync(stadium, cancellationToken);

        await _appContext.SaveChangesAsync(cancellationToken);

        return stadium.Id;
    }
}