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
    public class BookRepository
    {
        private readonly IMongoCollection<Book> _books;

        public BookRepository(DatabaseContext context)
        {
            _books = context.Books;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _books.Find(book => true).ToListAsync();
        }

        public async Task<Book> GetByIdAsync(string id)
        {
            return await _books.Find(book => book.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Book book)
        {
            await _books.InsertOneAsync(book);
        }

        public async Task UpdateAsync(string id, Book book)
        {
            await _books.ReplaceOneAsync(b => b.Id == id, book);
        }

        public async Task DeleteAsync(string id)
        {
            await _books.DeleteOneAsync(book => book.Id == id);
        }
    }
}
