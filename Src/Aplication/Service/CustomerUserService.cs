using Aplication.Interfaces.Application;
using Aplication.Interfaces.Infrastructure;
using Core.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Aplication.Service
{
    public class CustomerUserService(ICustomerUserRepository<CustomerUser> repository, IUserCacheRepository cacheRepository, ILogger<CustomerUserService> logger) : ICustomerUserService
    {
        private readonly ICustomerUserRepository<CustomerUser> _repository = repository;
        private readonly IUserCacheRepository _cacheRepository = cacheRepository;
        private readonly ILogger<CustomerUserService> _logger = logger;

        public async Task<string> CreateUser(CustomerUser user)
        {
            if (await UserIdDocumentExist(user.DocumentNumber))
                throw new Exception($"Usuario con documento {user.DocumentNumber} ya existe");

            // Asignacion de datos base creados por el sistema
            user.CustomerId = Guid.NewGuid().ToString();
            user.CreateDate = DateTime.UtcNow;

            string userId = await _repository.InsertDocument(user);

            if (!string.IsNullOrEmpty(userId))
                await _cacheRepository.SaveUserlist("");

            return userId;
        }

        public async Task<IEnumerable<CustomerUser>> GetAllUser()
        {
            string cachedData = await ComprobarCache();

            if (!string.IsNullOrEmpty(cachedData))
                return JsonSerializer.Deserialize<IEnumerable<CustomerUser>>(cachedData);

            IEnumerable<CustomerUser> userList = await _repository.GetAllAsync();

            _ = GuardarCache(JsonSerializer.Serialize(userList));

            return userList;
        }

        public async Task<CustomerUser> GetUserById(string customerId) 
        {
            if (!await UserCustomerIdExist(customerId))
                throw new Exception($"Usuario con CustomerId: {customerId} no encontrado");

            return await _repository.GetById(customerId);
        }

        public async Task<string> DeleteUserById(string customerId) 
        {
            if (!await UserCustomerIdExist(customerId))
                throw new Exception($"Usuario con id {customerId} no encontrado para eliminar");

            string userId = await _repository.DeleteById(customerId);

            if (!string.IsNullOrEmpty(userId))
                await _cacheRepository.SaveUserlist("");

            return userId;
        }

        public async Task<string> UpdateUserById(string customerId, CustomerUser user) 
        {
            if (!await UserCustomerIdExist(customerId))
                throw new Exception($"Usuario con CustomerId: {customerId} no encontrado para editar");

            string userId = await _repository.UpdateDocument(customerId, user);

            if (!string.IsNullOrEmpty(userId))
                await _cacheRepository.SaveUserlist("");

            return userId;
        }

        public async Task<CustomerUser> GetUserByIdDocument(string documentNumber) 
        {
            if (!await UserIdDocumentExist(documentNumber))
                throw new Exception($"Usuario con documento {documentNumber} no encontrado");

            return await _repository.GetByIdDocument(documentNumber);
        } 

        public async Task<string> GetIdByIdDocument(string documentNumber)
        {
            if(!await UserIdDocumentExist(documentNumber))
                throw new Exception($"Usuario con idDocument {documentNumber} no existe");

            return await _repository.GetIdByIdDocument(documentNumber);
        }

        public async Task<bool> UserCustomerIdExist(string customerId) => await _repository.DocumentExist(customerId);

        private async Task<bool> UserIdDocumentExist(string documentNumber) => await _repository.IdDocumentExist(documentNumber);

        private async Task<string> ComprobarCache() => await _cacheRepository.GetUserlist("Trinity_users");
        
        private async Task GuardarCache(string usersList)
        {
            _logger.LogInformation("Guardando usuarios obtenidos en cache");
            await _cacheRepository.SaveUserlist(usersList);
        }
    }
}
