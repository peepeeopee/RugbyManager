namespace RugbyManager.Domain.Entities;

public class Stadium : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    
}