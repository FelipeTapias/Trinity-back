using Core.Entities;

namespace Aplication.Interfaces.Application
{
    public interface IProductService
    {
        Task<string> CreateProduct(Product product);
        Task<IEnumerable<Product>> GetAllProductByCustomerId(string customerId);
        Task<string> UpdateStatusProduct(string productId, StatusProduct statusProduct);
        Task<bool> UpdateBalanceProduct(string productId, decimal value);
    }
}
