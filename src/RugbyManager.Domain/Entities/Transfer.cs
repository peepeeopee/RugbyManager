namespace RugbyManager.Domain.Entities;

public class Transfer : BaseEntity
{
    public int FromTeamId { get; set; }
    public int ToTeamId { get; set;}
    public int PlayerId { get; set; }
}