namespace Aplication.Interfaces.Infrastructure
{
    public interface IPaymentRepository<TEntity>
    {
        Task<string> CreatePayment(TEntity entity);
    }
}
