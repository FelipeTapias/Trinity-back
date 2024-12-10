using Aplication.Interfaces.Infrastructure;
using Infrastructure.Service.Mongo.Database.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Service.Mongo.Database.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public UserRepository(GenericContext context) 
            => _collection = context.GetCollection();


        public async Task<string> InsertDocument()
        {
            User user = new()
            {
                Name = "Felipe",
                Age = 22
            };

            BsonDocument doc = user.ToBsonDocument();
            await _collection.InsertOneAsync(doc);

            return doc["_id"].AsObjectId.ToString();
        }
    }

    public class User 
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
