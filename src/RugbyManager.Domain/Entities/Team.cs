namespace RugbyManager.Domain.Entities;

public class Team : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<TeamMembership> TeamMembers { get; set; } = new();
    public int? StadiumId { get; set; }
    public Stadium? Stadium { get; set; }
}