using Aplication.Interfaces.Infrastructure;
using Infrastructure.Service.Mongo.Database.Context;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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

        public async Task<IEnumerable<TEntity>> GetAllAsync() 
        { 
            IEnumerable<BsonDocument> result = await _collection.Find(new BsonDocument()).ToListAsync();

            IEnumerable<TEntity> entities = result.Select(doc => {
                doc.Remove("_id");
                return BsonSerializer.Deserialize<TEntity>(doc);
            });

            return entities;
        }

        public async Task<TEntity> GetById(string id) 
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));

            BsonDocument result = await _collection.Find(filter).FirstOrDefaultAsync();

            result.Remove("_id");

            return BsonSerializer.Deserialize<TEntity>(result);
        }

        public async Task<string> DeleteById(string id)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));

            await _collection.DeleteOneAsync(filter);

            return id;
        }

        public async Task<string> UpdateDocument(string id, TEntity entity)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));

            BsonDocument doc = entity.ToBsonDocument();
            doc["_id"] = ObjectId.Parse(id);

            await _collection.ReplaceOneAsync(filter, doc);

            return id;
        }

        public async Task<TEntity> GetByIdDocument(int idDocument)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("IdDocument", idDocument);

            BsonDocument result = await _collection.Find(filter).FirstOrDefaultAsync();

            result.Remove("_id");

            return BsonSerializer.Deserialize<TEntity>(result);
        }

        public async Task<string> GetIdByIdDocument(int idDocument)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("IdDocument", idDocument);
            ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Include("_id");

            BsonDocument result = await _collection.Find(filter).Project(projection).FirstOrDefaultAsync();

            return result["_id"].ToString();
        }

        public async Task<bool> IdDocumentExist(int idDocument)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("IdDocument", idDocument);

            return await _collection.CountDocumentsAsync(filter) > 0;
        }

        public async Task<bool> DocumentExist(string id)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));

            return await _collection.CountDocumentsAsync(filter) > 0;
        }
    }
}
