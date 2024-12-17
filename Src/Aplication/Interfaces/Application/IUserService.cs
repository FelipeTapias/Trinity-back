using Core.Entities;

namespace Aplication.Interfaces.Application
{
    public interface IUserService
    {
        Task<string> CreateUser(User user);
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(string id);
        Task<string> DeleteUserById(string id);
        Task<string> UpdateUserById(string id, User user);
        Task<User> GetUserByIdDocument(int idDocument);
    }
}
