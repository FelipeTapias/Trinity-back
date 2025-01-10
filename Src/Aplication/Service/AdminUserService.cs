using Aplication.Common.Helpers.Utils;
using Aplication.Interfaces.Application;
using Aplication.Interfaces.Infrastructure;
using Core.Entities;

namespace Aplication.Service
{
    public class AdminUserService(IAdminUserRepository<AdministratorUser> repository, ITokenGenerator tokenGenerator): IAdminUserService
    {
        private readonly IAdminUserRepository<AdministratorUser> _repository = repository;
        private readonly ITokenGenerator _tokenGenerator = tokenGenerator;

        public async Task<bool> CreateAdminUser(AdministratorUser administratorUser)
        {
            administratorUser.AdminId = Guid.NewGuid().ToString();
            administratorUser.CreateDate = DateTime.Now;
            administratorUser.Password = Encrypter.EncryptSHA256(administratorUser.Password);

            try
            {
                return await _repository.CreateAdminUser(administratorUser);
            }
            catch (Exception ex) {
                throw new Exception("Error Creando el Admin", ex);
            }
        }

        public async Task<string> LoginAdmin(string email, string password)
        {
            password = Encrypter.EncryptSHA256(password);

            if (!await _repository.isAdminUserValidate(email, password))
                throw new Exception("Correo y/o clave no son correctos");

            AdministratorUser administratorUser = await _repository.GetUserByCredentials(email, password);
        
            return _tokenGenerator.GenerateToken(administratorUser.ToString());
        }
    }
}
