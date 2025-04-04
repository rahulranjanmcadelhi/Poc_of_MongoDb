using DocumentDbPoc.Domain.Business;
using DocumentDbPoc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DocumentDbPoc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly BookRepository _bookRepository;
        private readonly AuthorRepository _authorRepository;

        public BooksController(BookRepository bookRepository, AuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAll()
        {
            return await _bookRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(string id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Book book)
        {
            var author = await _authorRepository.GetByIdAsync(book.AuthorId);
            if (author == null)
            {
                return BadRequest("Invalid AuthorId");
            }

            await _bookRepository.CreateAsync(book);
            author.BookIds.Add(book.Id);
            await _authorRepository.UpdateAsync(author.Id, author);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Book book)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            await _bookRepository.UpdateAsync(id, book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookRepository.DeleteAsync(id);

            var author = await _authorRepository.GetByIdAsync(book.AuthorId);
            if (author != null)
            {
                author.BookIds.Remove(book.Id);
                await _authorRepository.UpdateAsync(author.Id, author);
            }

            return NoContent();
        }
    }
}
