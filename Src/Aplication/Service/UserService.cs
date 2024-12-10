using Aplication.Interfaces.Infrastructure;
using Microsoft.Extensions.Logging;
using Core.Entities;
using Aplication.Interfaces.Application;

namespace Aplication.Service
{
    public class UserService: IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository<User> _repository;
        public UserService(ILogger<UserService> logger, IUserRepository<User> repository) 
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<string> CreateUser(User user)
        {
            return await _repository.InsertDocument(user);
        }
    }
}
