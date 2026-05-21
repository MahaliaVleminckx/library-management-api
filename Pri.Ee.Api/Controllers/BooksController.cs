using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pri.Ee.Core.DTOs;
using Pri.Ee.Core.Services;
using Pri.Ee.Core.Services.Interface;

namespace Pri.Ee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateDto dto)
        {
            var created = await _bookService.CreateAsync(dto);

            return CreatedAtAction
                (
                nameof(GetById),
                new {id = created.Id},
                created
                );
        }
        [HttpPut ("{id}")]
        public async Task<IActionResult> Update(int id, BookUpdateDto dto)
        {
            var success = await _bookService.UpdateAsync(id, dto);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bookService.DeleteAsync(id);

            if (!success)
            {
                return NotFound();
            }
            return NoContent() ;
        }
    }
       
}
