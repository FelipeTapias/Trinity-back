using Core.Entities;

namespace Aplication.Interfaces.Infrastructure
{
    public interface IProductRepository<TEntity> where TEntity : class
    {
        Task<string> InsertProduct(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllProductsByCustomer(string customerId);
        Task UpdateStatusProduct(string productId, StatusProduct entity);
        Task<bool> DocumentExist(string productId);
        Task<bool> UpdateBalanceProduct(string productId, decimal balance);
        Task<decimal> GetBalanceByProductId(string productId);
    }
}
