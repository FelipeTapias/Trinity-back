namespace Aplication.Interfaces.Infrastructure
{
    public interface IUserRepository<TEntity> where TEntity : class
    {
        Task<string> InsertDocument(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetById(string id);
        Task<string> DeleteById(string id);
        Task<string> UpdateDocument(string id, TEntity entity);
        Task<TEntity> GetByIdDocument(int idDocument);
        Task<bool> IdDocumentExist(int idDocument);
        Task<bool> DocumentExist(string id);
    }
}
