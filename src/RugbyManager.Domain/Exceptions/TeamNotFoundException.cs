namespace RugbyManager.Domain.Exceptions;

public class TeamNotFoundException : Exception
{
    public TeamNotFoundException(int teamId) : base($"Team with Id {teamId} not found")
    {
    }
}