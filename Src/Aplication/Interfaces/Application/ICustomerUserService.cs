using Core.Entities;

namespace Aplication.Interfaces.Application
{
    public interface ICustomerUserService
    {
        Task<string> CreateUser(CustomerUser user);
        Task<IEnumerable<CustomerUser>> GetAllUser();
        Task<CustomerUser> GetUserById(string customerId);
        Task<string> DeleteUserById(string customerId);
        Task<string> UpdateUserById(string customerId, CustomerUser user);
        Task<CustomerUser> GetUserByIdDocument(string documentNumber);
        Task<string> GetIdByIdDocument(string documentNumber);
        Task<bool> UserCustomerIdExist(string customerId);
    }
}
