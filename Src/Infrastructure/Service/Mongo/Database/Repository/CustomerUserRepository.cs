using Aplication.Interfaces.Infrastructure;
using Infrastructure.Service.Mongo.Database.Context;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Infrastructure.Service.Mongo.Database.Repository
{
    public class CustomerUserRepository<TEntity>: ICustomerUserRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public CustomerUserRepository(GenericContext context) 
            => _collection = context.GetCollectionCustomerUsers();

        public async Task<string> InsertDocument(TEntity entity)
        {
            BsonDocument doc = entity.ToBsonDocument();
            await _collection.InsertOneAsync(doc);

            return doc["CustomerId"].ToString();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() 
        { 
            IEnumerable<BsonDocument> result = await _collection.Find(new BsonDocument()).ToListAsync();

            IEnumerable<TEntity> entities = result.Select(doc => {
                doc.Remove("_id");
                return BsonSerializer.Deserialize<TEntity>(doc);
            });

            return entities;
        }

        public async Task<TEntity> GetById(string customerId) 
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("CustomerId", customerId);

            BsonDocument result = await _collection.Find(filter).FirstOrDefaultAsync();

            result.Remove("_id");

            return BsonSerializer.Deserialize<TEntity>(result);
        }

        public async Task<string> DeleteById(string customerId)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("CustomerId", customerId);

            await _collection.DeleteOneAsync(filter);

            return customerId;
        }

        public async Task<string> UpdateDocument(string customerId, TEntity entity)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("CustomerId", customerId);

            BsonDocument doc = entity.ToBsonDocument();
   
            await _collection.ReplaceOneAsync(filter, doc);

            return customerId;
        }

        public async Task<TEntity> GetByIdDocument(string documentNumber)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("DocumentNumber", documentNumber);

            BsonDocument result = await _collection.Find(filter).FirstOrDefaultAsync();

            result.Remove("_id");

            return BsonSerializer.Deserialize<TEntity>(result);
        }

        public async Task<string> GetIdByIdDocument(string documentNumber)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("DocumentNumber", documentNumber);
            ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Include("CustomerId");

            BsonDocument result = await _collection.Find(filter).Project(projection).FirstOrDefaultAsync();

            return result["CustomerId"].ToString();
        }

        public async Task<bool> IdDocumentExist(string documentNumber)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("DocumentNumber", documentNumber);

            return await _collection.CountDocumentsAsync(filter) > 0;
        }

        public async Task<bool> DocumentExist(string customerId)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("CustomerId", customerId);

            return await _collection.CountDocumentsAsync(filter) > 0;
        }
    }
}
