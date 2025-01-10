namespace Aplication.Interfaces.Infrastructure
{
    public interface ITokenGenerator
    {
        string GenerateToken(string fullName);
    }
}
