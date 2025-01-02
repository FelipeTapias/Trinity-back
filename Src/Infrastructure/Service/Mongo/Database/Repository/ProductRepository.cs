using Aplication.Interfaces.Infrastructure;
using Infrastructure.Service.Mongo.Database.Context;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Infrastructure.Service.Mongo.Database.Repository
{
    public class ProductRepository<TEntity>: IProductRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public ProductRepository(GenericContext context) => 
            _collection = context.GetCollectionProducts();

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


    }
}
