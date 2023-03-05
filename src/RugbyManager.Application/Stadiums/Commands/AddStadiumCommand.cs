using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Application.Common.Models;
using RugbyManager.Domain.Entities;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Stadiums.Commands;

public class AddStadiumCommand : IRequest<int>, IMapFrom<AddStadiumRequest>
{
    public string Name { get; init; }
    public int Capacity { get; init; }
    
}

public class AddStadiumCommandHandler : IRequestHandler<AddStadiumCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public AddStadiumCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddStadiumCommand request, CancellationToken cancellationToken)
    {
        //if (await _appDbContext.Stadiums
        //                     .FirstOrDefaultAsync(t =>
        //                             t.Name == request.Name,
        //                         cancellationToken) is not null)
        //{
        //    throw new StadiumAlreadyExistsException(request.Name);
        //}

        Stadium stadium = new()
        {
            Name = request.Name,
            Capacity = request.Capacity
        };

        await _appDbContext.Stadiums.AddAsync(stadium, cancellationToken);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return stadium.Id;
    }
}