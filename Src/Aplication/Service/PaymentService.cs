using Aplication.Interfaces.Application;
using Aplication.Interfaces.Infrastructure;
using Core.Entities;

namespace Aplication.Service
{
    public class PaymentService(IPaymentRepository<Payment> repository, IProductService productService): IPaymentService
    {
        private readonly IPaymentRepository<Payment> _repository = repository;
        private readonly IProductService _productService = productService;

        public async Task<string> CreatePayment(Payment payment)
        {
            await _productService.ProductExist(payment.ProductId);

            if(await _productService.IsProductCanceled(payment.ProductId))
                throw new Exception("El producto  se encuentra cancelado");

            if (!await _productService.IsPaymentValid(payment.ProductId, payment.Value))
                throw new Exception("El pago a realizar no es valido");

            decimal newBalance = await _productService.GetBalanceByProductId(payment.ProductId) - payment.Value;
            
            await _productService.UpdateBalanceProduct(payment.ProductId, newBalance);

            payment.PaymentId = Guid.NewGuid().ToString();
            payment.CreateDate = DateTime.UtcNow;

            return await _repository.CreatePayment(payment);   
        }
    }
}
