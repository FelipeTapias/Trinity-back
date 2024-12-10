using Aplication.Interfaces.Infrastructure;
using Infrastructure.Service.Mongo.Database.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Service.Mongo.Database.Repository
{
    public class UserRepository<TEntity>: IUserRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public UserRepository(GenericContext context) 
            => _collection = context.GetCollection();


        public async Task<string> InsertDocument(TEntity entity)
        {
            BsonDocument doc = entity.ToBsonDocument();
            await _collection.InsertOneAsync(doc);

            return doc["_id"].AsObjectId.ToString();
        }
    }
}
