using Core.Entities;

namespace Aplication.Interfaces.Application
{
    public interface IUserService
    {
        Task<string> CreateUser(User user);
    }
}
