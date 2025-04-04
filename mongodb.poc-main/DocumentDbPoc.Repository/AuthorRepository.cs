using DocumentDbPoc.Domain.Business;
using DocumentDbPoc.Persistance;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDbPoc.Repository
{
    public class AuthorRepository
    {
        private readonly IMongoCollection<Author> _authors;

        public AuthorRepository(DatabaseContext context)
        {
            _authors = context.Authors;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _authors.Find(author => true).ToListAsync();
        }

        public async Task<Author> GetByIdAsync(string id)
        {
            return await _authors.Find(author => author.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Author author)
        {
            await _authors.InsertOneAsync(author);
        }

        public async Task UpdateAsync(string id, Author author)
        {
            await _authors.ReplaceOneAsync(a => a.Id == id, author);
        }

        public async Task DeleteAsync(string id)
        {
            await _authors.DeleteOneAsync(author => author.Id == id);
        }
    }
}
