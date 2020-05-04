using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using TravelRecordApp.Model;

namespace TravelRecordApp.Services
{
    public static class MongoService
    {
        static IMongoCollection<User> todoItemsCollection;
        readonly static string dbName = "TravelRecordDB";
        readonly static string collectionName = "User";
        static MongoClient client;

        static IMongoCollection<User> UsersCollection
        {
            get
            {
                if (client == null || todoItemsCollection == null)
                {
                    var conn = "mongodb://travelrec0rdapp:A5F0eEbrdO8pnUyb3G9bHtpdp5jT3LgToQetbDhdzFtREYkC6teVSSc0C1GUdoePjeWewvadwEquZO8ziBtGQQ==@travelrec0rdapp.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@travelrec0rdapp@";
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                        new MongoUrl(conn)
                    );

                    settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

                    client = new MongoClient(settings);
                    var db = client.GetDatabase(dbName);

                    var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                    todoItemsCollection = db.GetCollection<User>(collectionName, collectionSettings);
                }

                return todoItemsCollection;
            }
        }

        public async static Task<List<User>> GetAllUsers()
        {
            var allItems = await UsersCollection
                .Find(new BsonDocument())
                .ToListAsync();

            return allItems;
        }

        public async static Task<List<User>> LoginByEmail(string email)
        {
            var results = await UsersCollection
                            .AsQueryable()
                            .Where(tdi => tdi.Email.Contains(email))
                            .Take(10)
                            .ToListAsync();

            return results;
        }

        public async static Task InsertUser(User item)
        {
            await UsersCollection.InsertOneAsync(item);
        }

        public async static Task<bool> DeleteUser(User item)
        {
            var result = await UsersCollection.DeleteOneAsync(tdi => tdi.Id == item.Id);

            return result.IsAcknowledged && result.DeletedCount == 1;
        }
    }
}
