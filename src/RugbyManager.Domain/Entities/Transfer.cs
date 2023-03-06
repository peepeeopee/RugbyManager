namespace RugbyManager.Domain.Entities;

public class Transfer : BaseEntity
{
    public int? FromTeamId { get; set; }
    public Team? FromTeam { get; set; }
    public int ToTeamId { get; set;}
    public Team ToTeam { get; set; }
    public int PlayerId { get; set; }
    public Player Player { get; set; }
}