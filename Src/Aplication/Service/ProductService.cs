using Aplication.Interfaces.Application;
using Aplication.Interfaces.Infrastructure;
using Core.Entities;
using Core.Enums;

namespace Aplication.Service
{
    public class ProductService(IProductRepository<Product> repository, ICustomerUserService customerUserService): IProductService
    {
        private readonly IProductRepository<Product> _repository = repository;
        private readonly ICustomerUserService _customerUserService = customerUserService;

        public async Task<string> CreateProduct(Product product)
        {
            await CustomerExist(product.CustomerId);

            if (product.Price <= 0)
                throw new Exception("El precio del producto no puedo ser igual o menos a cero");

            if (product.Status != ProductStatus.New)
                throw new Exception("Estado no permitido para la creación de un producto");

            product.ProductId = Guid.NewGuid().ToString();
            product.CreateDate = DateTime.UtcNow;
            product.Balance = product.Price;

            string idProduct = await _repository.InsertProduct(product);

            return idProduct;
        }

        public async Task<IEnumerable<Product>> GetAllProductByCustomerId(string customerId)
        {
            await CustomerExist(customerId);

            return await _repository.GetAllProductsByCustomer(customerId);
        }

        private async Task<bool> CustomerExist(string customerId)
        {
            if (!await _customerUserService.UserCustomerIdExist(customerId))
                throw new Exception($"El cliente con CustomerId {customerId} no existe");

            return true;
        }

    }
}
