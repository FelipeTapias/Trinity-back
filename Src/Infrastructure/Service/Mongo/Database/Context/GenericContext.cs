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

        public IMongoCollection<BsonDocument> GetCollectionCustomerUsers() 
            =>_database.GetCollection<BsonDocument>("Customers");

        public IMongoCollection<BsonDocument> GetCollectionProducts()
            => _database.GetCollection<BsonDocument>("Products");

        public IMongoCollection<BsonDocument> GetCollectionPayments()
            => _database.GetCollection<BsonDocument>("Payments");

        public IMongoCollection<BsonDocument> GetCollectionAdminUsers()
            => _database.GetCollection<BsonDocument>("AdminUsers");
    }
}
