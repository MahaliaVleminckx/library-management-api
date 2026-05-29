using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pri.Ee.Client.Models.Books;
using Pri.Ee.Client.Services;

namespace Pri.Ee.Client.Controllers
{
    public class BooksController : Controller
    {
      private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAll();
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var books = await _bookService.GetById(id);
            return View(books);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() 
        { 
                return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await _bookService.Create(model);

            if (!success)
            {
                ViewBag.Error = "Could not create book";
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetById(id);
            var model = new CreateBookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = book.AuthorId,
                CategoryId = book.CategoryId
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id,CreateBookViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _bookService.Update(id, model);

            if (!success)
            {
                ViewBag.Error = "Update failed";
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bookService.Delete(id);
            if (!success)
            {
                TempData["Error"] = "Delete failed";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
