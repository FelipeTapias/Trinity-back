using Aplication.Interfaces.Application;
using Aplication.Interfaces.Infrastructure;
using Core.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Aplication.Service
{
    public class UserService(IUserRepository<User> repository, IUserCacheRepository cacheRepository, ILogger<UserService> logger) : IUserService
    {
        private readonly IUserRepository<User> _repository = repository;
        private readonly IUserCacheRepository _cacheRepository = cacheRepository;
        private readonly ILogger<UserService> _logger = logger;

        public async Task<string> CreateUser(User user)
        {
            if (await UserIdDocumentExist(user.IdDocument))
                throw new Exception($"Usuario con documento {user.IdDocument} ya existe");

            string userId = await _repository.InsertDocument(user);

            if (!string.IsNullOrEmpty(userId))
                await _cacheRepository.SaveUserlist("");

            return userId;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            string cachedData = await ComprobarCache();

            if (!string.IsNullOrEmpty(cachedData))
                return JsonSerializer.Deserialize<IEnumerable<User>>(cachedData);

            IEnumerable<User> userList = await _repository.GetAllAsync();

            _ = GuardarCache(JsonSerializer.Serialize(userList));

            return userList;
        }

        public async Task<User> GetUserById(string id) 
        {
            if (!await UserIdExist(id))
                throw new Exception($"Usuario con id {id} no encontrado");

            return await _repository.GetById(id);
        }

        public async Task<string> DeleteUserById(string id) 
        {
            if (!await UserIdExist(id))
                throw new Exception($"Usuario con id {id} no encontrado para eliminar");

            string userId = await _repository.DeleteById(id);

            if (!string.IsNullOrEmpty(userId))
                await _cacheRepository.SaveUserlist("");

            return userId;
        }

        public async Task<string> UpdateUserById(string id, User user) 
        {
            if (!await UserIdExist(id))
                throw new Exception($"Usuario con id {id} no encontrado para editar");

            string userId = await _repository.UpdateDocument(id, user);

            if (!string.IsNullOrEmpty(userId))
                await _cacheRepository.SaveUserlist("");

            return userId;
        }

        public async Task<User> GetUserByIdDocument(int idDocument) 
        {
            if (!await UserIdDocumentExist(idDocument))
                throw new Exception($"Usuario con documento {idDocument} no encontrado");

            return await _repository.GetByIdDocument(idDocument);
        } 

        public async Task<string> GetIdByIdDocument(int idDocument)
        {
            if(!await UserIdDocumentExist(idDocument))
                throw new Exception($"Usuario con idDocument {idDocument} no existe");

            return await _repository.GetIdByIdDocument(idDocument);
        }

        private async Task<bool> UserIdDocumentExist(int idDocument) => await _repository.IdDocumentExist(idDocument);

        private async Task<bool> UserIdExist(string id) => await _repository.DocumentExist(id);

        private async Task<string> ComprobarCache() => await _cacheRepository.GetUserlist("Trinity_users");
        
        private async Task GuardarCache(string usersList)
        {
            _logger.LogInformation("Guardando usuarios obtenidos en cache");
            await _cacheRepository.SaveUserlist(usersList);
        }
    }
}
