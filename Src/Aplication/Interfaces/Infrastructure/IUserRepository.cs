namespace Aplication.Interfaces.Infrastructure
{
    public interface IUserRepository
    {
        Task<string> InsertDocument();
    }
}
