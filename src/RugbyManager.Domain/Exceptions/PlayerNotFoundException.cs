namespace RugbyManager.Domain.Exceptions;

public class PlayerNotFoundException : Exception
{
    public PlayerNotFoundException(int playerId) : base($"Player with Id {playerId} was not found")
    {
        
    }
}