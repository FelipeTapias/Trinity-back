using Aplication.Interfaces.Infrastructure;
using Infrastructure.Service.Mongo.Database.Context;
using MongoDB.Bson;
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

            return doc["_id"].AsObjectId.ToString();
        }


    }
}
