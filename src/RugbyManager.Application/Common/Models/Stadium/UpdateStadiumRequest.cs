namespace RugbyManager.Application.Common.Models.Stadium;

public class UpdateStadiumRequest
{
    public int StadiumId { get; init; }
    public string? Name { get; init; }
    public int? Capacity { get; set; }
}