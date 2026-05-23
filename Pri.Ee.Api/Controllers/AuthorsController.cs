using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pri.Ee.Core.DTOs;
using Pri.Ee.Core.Services.Interface;

namespace Pri.Ee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(AuthorCreateDto dto)
        {
            var created = await _authorService.CreateAsync(dto);

            return CreatedAtAction
                (
                nameof(GetById),
                new { id = created.Id },
                created
                );
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AuthorUpdateDto dto)
        {
            var success = await _authorService.UpdateAsync(id, dto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var success = await _authorService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();

        }


        [HttpGet("{id}/books")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBooksByAuthor(int id)
        {
            var books = await _authorService.GetBooksByAuthorAsync(id);

            if (books == null || !books.Any())
            {
                return NotFound();
            }
            return Ok(books);
        }
    }
    
}
