namespace Aplication.Interfaces.Infrastructure
{
    public interface IProductRepository<TEntity> where TEntity : class
    {
        Task<string> InsertProduct(TEntity entity);
    }
}
