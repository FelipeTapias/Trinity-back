using Aplication.Interfaces.Infrastructure;
using Microsoft.Extensions.Logging;
using Core.Entities;
using Aplication.Interfaces.Application;

namespace Aplication.Service
{
    public class UserService: IUserService
    {
        private readonly IUserRepository<User> _repository;
        public UserService(IUserRepository<User> repository) 
        {
            _repository = repository;
        }

        public async Task<string> CreateUser(User user) => await _repository.InsertDocument(user);

        public async Task<IEnumerable<User>> GetAllUser() => await _repository.GetAllAsync();

        public async Task<User> GetUserById(string id) => await _repository.GetById(id);

        public async Task<string> DeleteUserById(string id) => await _repository.DeleteById(id);
        public async Task<string> UpdateUserById(string id, User user) => await _repository.UpdateDocument(id, user);
        
    }
}
