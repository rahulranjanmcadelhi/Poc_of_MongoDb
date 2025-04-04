using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDbPoc.Service.DocumentDbServices
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;
        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;

            var connString = _configuration.GetConnectionString("DbConnection");
            var mongoUrl = MongoUrl.Create(connString);

            var settings = MongoClientSettings.FromConnectionString(connString);
            settings.ConnectTimeout = TimeSpan.FromSeconds(60);
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(60);
            settings.SocketTimeout = TimeSpan.FromSeconds(60);


            var mongoClient = new MongoClient(settings);
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase? Database => _database;
    }
}
