using RugbyManager.Application.Common.Mapping;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.Players.Queries;

public class PlayerDto : IMapFrom<Player>
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public double? Height { get; set; }
}