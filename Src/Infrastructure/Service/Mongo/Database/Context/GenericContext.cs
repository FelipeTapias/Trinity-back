using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Service.Mongo.Database.Context
{
    public class GenericContext
    {
        public readonly string _collectionName;
        private readonly IMongoDatabase _database;

        public GenericContext(string collectionName, string connectionString, string databaseName)
        {
            MongoClient client = new MongoClient(connectionString);
            _collectionName = collectionName;
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<BsonDocument> GetCollection() 
            =>_database.GetCollection<BsonDocument>(_collectionName);
        
    }
}
