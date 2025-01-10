using Aplication.Interfaces.Infrastructure;
using Infrastructure.Service.Mongo.Database.Context;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Infrastructure.Service.Mongo.Database.Repository
{
    public  class AdminUserRepository<TEntity>(GenericContext context): IAdminUserRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<BsonDocument> _collection = context.GetCollectionAdminUsers();

        public async Task<bool> CreateAdminUser(TEntity entity)
        {
            BsonDocument doc = entity.ToBsonDocument();

            await _collection.InsertOneAsync(doc);

            if (string.IsNullOrEmpty(doc["_id"].AsObjectId.ToString()))
                return false;

            return true;
        }

        public async Task<bool> isAdminUserValidate(string email, string password)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter
                                                                            .And(Builders<BsonDocument>.Filter.Eq("Email", email),
                                                                                 Builders<BsonDocument>.Filter.Eq("Password", password));

            return await _collection.CountDocumentsAsync(filter) > 0;
        }

        public async Task<TEntity> GetUserByCredentials(string email, string password)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter
                                                                            .And(Builders<BsonDocument>.Filter.Eq("Email", email),
                                                                                 Builders<BsonDocument>.Filter.Eq("Password", password));

            BsonDocument result = await _collection.Find(filter).FirstOrDefaultAsync();

            result.Remove("_id");

            return BsonSerializer.Deserialize<TEntity>(result);
        }
    }
}
