namespace RugbyManager.Domain.Entities;

public class Team : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<Player> Players { get; set; } = new();
    public Stadium? Stadium { get; set; }
}