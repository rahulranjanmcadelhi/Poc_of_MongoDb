using DocumentDbPoc.Domain.Business;
using DocumentDbPoc.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentDbPoc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorRepository _authorRepository;
        private readonly BookRepository _bookRepository;

        public AuthorsController(AuthorRepository authorRepository, BookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAll()
        {
            return await _authorRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetById(string id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return author;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Author author)
        {
            await _authorRepository.CreateAsync(author);
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Author author)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(id);
            if (existingAuthor == null)
            {
                return NotFound();
            }
            await _authorRepository.UpdateAsync(id, author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            await _authorRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
