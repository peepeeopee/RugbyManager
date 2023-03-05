namespace RugbyManager.Application.Common.Models.Player;

public class UpdatePlayerRequest
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public double? Height { get; set; }
}