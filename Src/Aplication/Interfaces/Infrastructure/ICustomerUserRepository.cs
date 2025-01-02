namespace Aplication.Interfaces.Infrastructure
{
    public interface ICustomerUserRepository<TEntity> where TEntity : class
    {
        Task<string> InsertDocument(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetById(string customerId);
        Task<string> DeleteById(string customerId);
        Task<string> UpdateDocument(string customerId, TEntity entity);
        Task<TEntity> GetByIdDocument(string documentNumber);
        Task<bool> IdDocumentExist(string documentNumber);
        Task<bool> DocumentExist(string customerId);
        Task<string> GetIdByIdDocument(string documentNumber);
    }
}
