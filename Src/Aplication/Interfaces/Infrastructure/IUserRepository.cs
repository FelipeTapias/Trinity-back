namespace Aplication.Interfaces.Infrastructure
{
    public interface IUserRepository<TEntity> where TEntity : class
    {
        Task<string> InsertDocument(TEntity entity);
    }
}
