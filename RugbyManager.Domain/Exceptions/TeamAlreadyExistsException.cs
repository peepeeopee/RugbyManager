namespace RugbyManager.Domain.Exceptions;

public class TeamAlreadyExistsException : Exception
{
    public TeamAlreadyExistsException(string teamName) : base(
        $"A team with the name {teamName} already exists")
    {
    }
}