using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RugbyManager.Application.Common.Interfaces;

namespace RugbyManager.Application.Transfers.Queries;

public class GetTransfersQuery : IRequest<List<TransferDto>>
{
}

public class GetTransfersQueryHandler : IRequestHandler<GetTransfersQuery, List<TransferDto>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetTransfersQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<List<TransferDto>> Handle(
        GetTransfersQuery request,
        CancellationToken cancellationToken) =>
        await _appDbContext.Transfers
                           .ProjectTo<TransferDto>(_mapper.ConfigurationProvider)
                           .ToListAsync(cancellationToken);
}