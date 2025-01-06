using Core.Entities;

namespace Aplication.Interfaces.Application
{
    public interface IPaymentService
    {
        Task<string> CreatePayment(Payment payment);
    }
}
