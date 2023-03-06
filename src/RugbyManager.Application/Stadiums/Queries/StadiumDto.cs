using RugbyManager.Application.Common.Mapping;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.Stadiums.Queries;

public class StadiumDto : IMapFrom<Stadium>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? Capacity { get; set; }
}