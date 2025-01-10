namespace Aplication.Interfaces.Infrastructure
{
    public interface IAdminUserRepository<TEntity> where TEntity : class 
    {
        Task<bool> CreateAdminUser(TEntity entity);
        Task<bool> isAdminUserValidate(string email, string password);
        Task<TEntity> GetUserByCredentials(string email, string password);
    }
}
