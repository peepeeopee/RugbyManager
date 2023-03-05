namespace RugbyManager.Domain.Exceptions;

public class StadiumNotFoundException : Exception
{
    public StadiumNotFoundException(int stadiumId) : base($"Stadium with Id {stadiumId} not found")
    {
    }
}