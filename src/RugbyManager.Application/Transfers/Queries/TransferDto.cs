using RugbyManager.Application.Common.Mapping;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.Transfers.Queries;

public class TransferDto : IMapFrom<Transfer>
{
    public int Id { get; set; }
    public int FromTeamId { get; set; }
    public int ToTeamId { get; set; }
    public int PlayerId { get; set; }
}