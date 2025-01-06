using Aplication.Interfaces.Infrastructure;
using Infrastructure.Service.Mongo.Database.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Service.Mongo.Database.Repository
{
    public class PaymentRepository<TEntity>(GenericContext context) : IPaymentRepository<TEntity> where TEntity: class
    {
        private readonly IMongoCollection<BsonDocument> _collection = context.GetCollectionPayments();

        public async Task<string> CreatePayment(TEntity entity)
        {
            BsonDocument doc = entity.ToBsonDocument();

            await _collection.InsertOneAsync(doc);

            return doc["PaymentId"].ToString();
        }
    }
}
