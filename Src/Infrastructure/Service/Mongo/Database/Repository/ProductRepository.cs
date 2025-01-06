using Aplication.Interfaces.Infrastructure;
using Core.Entities;
using Infrastructure.Service.Mongo.Database.Context;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Infrastructure.Service.Mongo.Database.Repository
{
    public class ProductRepository<TEntity>(GenericContext context) : IProductRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<BsonDocument> _collection = context.GetCollectionProducts();

        public async Task<string> InsertProduct(TEntity entity)
        {
            BsonDocument doc = entity.ToBsonDocument();
            await _collection.InsertOneAsync(doc);

            return doc["ProductId"].ToString();
        }

        public async Task<IEnumerable<TEntity>> GetAllProductsByCustomer(string customerId)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("CustomerId", customerId);

            IEnumerable<BsonDocument> result = await _collection.Find(filter).ToListAsync();

            IEnumerable<TEntity> entities = result.Select(doc => {
                doc.Remove("_id");
                return BsonSerializer.Deserialize<TEntity>(doc);
            });

            return entities;
        }

        public async Task UpdateStatusProduct(string productId, StatusProduct entity)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("ProductId", productId);

            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.AddToSet("Statuses", entity);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task<bool> DocumentExist(string productId)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("ProductId", productId);

            return await _collection.CountDocumentsAsync(filter) > 0;
        }

        public async Task<bool> UpdateBalanceProduct(string productId, decimal balance)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("ProductId", productId);

            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set("Balance", balance);

            UpdateResult result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;
        }

        public async Task<decimal> GetBalanceByProductId(string productId)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("ProductId", productId);
            ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Include("Balance");

            BsonDocument result = await _collection.Find(filter).Project(projection).FirstOrDefaultAsync();

            return decimal.Parse(result["Balance"].ToString());
        }

    }
}
