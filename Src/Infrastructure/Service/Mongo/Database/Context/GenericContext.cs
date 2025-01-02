using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Service.Mongo.Database.Context
{
    public class GenericContext
    {
        private readonly IMongoDatabase _database;

        public GenericContext(string connectionString, string databaseName)
        {
            MongoClient client = new MongoClient(connectionString);

            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<BsonDocument> GetCollectionUsers() 
            =>_database.GetCollection<BsonDocument>("Users");

        public IMongoCollection<BsonDocument> GetCollectionProducts()
            => _database.GetCollection<BsonDocument>("Products");
    }
}
