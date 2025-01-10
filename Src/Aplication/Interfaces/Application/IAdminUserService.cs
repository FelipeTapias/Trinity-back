using Core.Entities;

namespace Aplication.Interfaces.Application
{
    public interface IAdminUserService
    {
        Task<bool> CreateAdminUser(AdministratorUser administratorUser);
        Task<string> LoginAdmin(string email, string password);
    }
}
