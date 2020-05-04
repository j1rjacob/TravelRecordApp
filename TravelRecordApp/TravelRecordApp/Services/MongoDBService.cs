using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace TravelRecordApp.Services
{
    public class MongoDBService
    {
        private readonly IMongoDatabase _db;
        private readonly MongoCollectionSettings _collectionSettings;

        public MongoDBService(string database)
        {
            var conn = "mongodb://travelrec0rdapp:A5F0eEbrdO8pnUyb3G9bHtpdp5jT3LgToQetbDhdzFtREYkC6teVSSc0C1GUdoePjeWewvadwEquZO8ziBtGQQ==@travelrec0rdapp.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@travelrec0rdapp@";
            MongoClientSettings settings = MongoClientSettings.FromUrl(
                new MongoUrl(conn)
            );

            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
            var client = new MongoClient(settings);
            _db = client.GetDatabase(database);

            _collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
        }

        public async Task InsertRecord<T>(string table, T record)
        {
            var collection = _db.GetCollection<T>(table, _collectionSettings);
            await collection.InsertOneAsync(record);
        }

        public async Task<List<T>> LoadRecordsAsync<T>(string table)
        {
            var collection = _db.GetCollection<T>(table, _collectionSettings);

            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<T> LoadRecordById<T>(string table, Guid id)
        {
            var collection = _db.GetCollection<T>(table, _collectionSettings);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> LoadRecordByEmail<T>(string table, string email)
        {
            var collection = _db.GetCollection<T>(table, _collectionSettings);
            var filter = Builders<T>.Filter.Eq("Email", email);

            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = _db.GetCollection<T>(table, _collectionSettings);

            var result = await collection.ReplaceOneAsync(new BsonDocument("_id", id),
                record, new ReplaceOptions { IsUpsert = true });
        }

        public async void DeleteRecord<T>(string table, Guid id)
        {
            var collection = _db.GetCollection<T>(table, _collectionSettings);
            var filter = Builders<T>.Filter.Eq("Id", id);
            await collection.DeleteOneAsync(filter);
        }
    }
}
