using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;
using RugbyManager.Domain.Exceptions;

namespace RugbyManager.Application.Stadiums.Queries;

public class GetStadiumByIdQuery : IRequest<StadiumDto>
{
    public int StadiumId { get; set; }
}

public class GetStadiumByIdQueryHandler : IRequestHandler<GetStadiumByIdQuery, StadiumDto>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetStadiumByIdQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<StadiumDto> Handle(
        GetStadiumByIdQuery request,
        CancellationToken cancellationToken)
    {
        var stadium =
            await _appDbContext.Stadiums.FirstOrDefaultAsync(s => s.Id == request.StadiumId,
                cancellationToken);

        if (stadium is null)
        {
            throw new StadiumNotFoundException(request.StadiumId);
        }

        return _mapper.Map<StadiumDto>(stadium);
    }
}