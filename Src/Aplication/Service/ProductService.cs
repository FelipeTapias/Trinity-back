﻿using Aplication.Interfaces.Application;
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

            if (product.Statuses[0].Status != ProductStatus.New)
                throw new Exception("Estado no permitido para la creación de un producto");

            product.ProductId = Guid.NewGuid().ToString();
            product.CreateDate = DateTime.UtcNow;
            product.Statuses[0].CreateDate = DateTime.UtcNow;
            product.Balance = product.Price;

            string idProduct = await _repository.InsertProduct(product);

            return idProduct;
        }

        public async Task<IEnumerable<Product>> GetAllProductByCustomerId(string customerId)
        {
            await CustomerExist(customerId);

            return await _repository.GetAllProductsByCustomer(customerId);
        }

        public async Task<string> UpdateStatusProduct(string productId, StatusProduct statusProduct)
        {
            if (!Enum.IsDefined(typeof(ProductStatus), statusProduct.Status))
                throw new Exception($"Estado producto: {statusProduct.Status} no es válido");

            await ProductExist(productId);

            statusProduct.CreateDate = DateTime.UtcNow;
            await _repository.UpdateStatusProduct(productId, statusProduct);
            return productId;
        }

        public async Task<bool> UpdateBalanceProduct(string productId, decimal value)
        {
            return await _repository.UpdateBalanceProduct(productId, value);
        }

        public async Task<bool> ProductExist(string productId)
        {
            if (!await _repository.DocumentExist(productId))
                throw new Exception($"El producto: {productId} no existe");

            return true;
        }

        public async Task<bool> IsPaymentValid(string productId, decimal value)
        {
            decimal balance = await GetBalanceByProductId(productId);

            if (value > balance || value == 0)
                return false;

            return true;
        }

        public async Task<decimal> GetBalanceByProductId(string productId)
        {
            return await _repository.GetBalanceByProductId(productId);
        }

        public async Task<bool> IsProductCanceled(string productId)
        {
            Product product = await _repository.GetProductByProductId(productId);

            if (product.GetLastStatus().Status == ProductStatus.Canceled)
                return true;

            return false;
        }

        private async Task<bool> CustomerExist(string customerId)
        {
            if (!await _customerUserService.UserCustomerIdExist(customerId))
                throw new Exception($"El cliente con CustomerId {customerId} no existe");

            return true;
        }
    }
}
