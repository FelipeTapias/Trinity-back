using Aplication.Interfaces.Application;
using Aplication.Interfaces.Infrastructure;
using Core.Entities;

namespace Aplication.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<User> _repository;
        public UserService(IUserRepository<User> repository) => _repository = repository;

        public async Task<string> CreateUser(User user)
        {
            if (await UserIdDocumentExist(user.IdDocument))
                throw new Exception($"Usuario con documento {user.IdDocument} ya existe");

            return await _repository.InsertDocument(user);
        }

        public async Task<IEnumerable<User>> GetAllUser() => await _repository.GetAllAsync();

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

            return await _repository.DeleteById(id);
        }

        public async Task<string> UpdateUserById(string id, User user) 
        {
            if (!await UserIdExist(id))
                throw new Exception($"Usuario con id {id} no encontrado para editar");

            return await _repository.UpdateDocument(id, user);
        }

        public async Task<User> GetUserByIdDocument(int idDocument) 
        {
            if (!await UserIdDocumentExist(idDocument))
                throw new Exception($"Usuario con documento {idDocument} no encontrado");

            return await _repository.GetByIdDocument(idDocument);
        } 

        private async Task<bool> UserIdDocumentExist(int idDocument) => await _repository.IdDocumentExist(idDocument);

        private async Task<bool> UserIdExist(string id) => await _repository.DocumentExist(id);
    }
}
