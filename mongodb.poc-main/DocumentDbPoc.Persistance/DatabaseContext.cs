using DocumentDbPoc.Domain.Business;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDbPoc.Persistance
{
    public class DatabaseContext
    {
        private readonly IMongoDatabase _database;

        public DatabaseContext(IOptions<AppSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Author> Authors => _database.GetCollection<Author>("Authors");
        public IMongoCollection<Book> Books => _database.GetCollection<Book>("Books");
    }
}
