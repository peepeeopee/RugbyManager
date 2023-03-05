namespace RugbyManager.Application.Common.Models.Team;

public class UpdateTeamRequest
{
    public int TeamId { get; set; }
    public string? Name { get; set; }
    public int? StadiumId { get; set; }
}