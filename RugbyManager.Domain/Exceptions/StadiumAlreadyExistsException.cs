namespace RugbyManager.Domain.Exceptions;

public class StadiumAlreadyExistsException : Exception
{
    public StadiumAlreadyExistsException(string stadiumName) : base($"A team with the name {stadiumName} already exists")
    {
        
    }
}