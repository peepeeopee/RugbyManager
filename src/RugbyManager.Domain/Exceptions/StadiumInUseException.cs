namespace RugbyManager.Domain.Exceptions;

public class StadiumInUseException : Exception
{
    public StadiumInUseException(string stadiumName) : base($"{stadiumName} is currently still being used by teams. Remove the stadium from the teams to removing the stadium")
    {
        
    }
}