using RugbyManager.Application.Common.Mapping;
using RugbyManager.Domain.Entities;

namespace RugbyManager.Application.Teams.Queries;

public class TeamDto : IMapFrom<Team>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? StadiumId { get; set; }
    public string? StadiumName { get; set; }
    public int? StadiumCapacity { get; set; }

    public IList<TeamMemberDto> TeamMembers { get; set; } = new List<TeamMemberDto>();
}