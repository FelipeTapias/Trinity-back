using Aplication.Interfaces.Application;
using Aplication.Interfaces.Infrastructure;
using Core.Entities;

namespace Aplication.Service
{
    public class ProductService(IProductRepository<Product> repository): IProductService
    {
        private readonly IProductRepository<Product> _repository = repository;

        public async Task<string> CreateProduct(Product product)
        {
            product.IdProduct = Guid.NewGuid().ToString();

            string idProduct = await _repository.InsertProduct(product);

            return idProduct;
        }

    }
}
