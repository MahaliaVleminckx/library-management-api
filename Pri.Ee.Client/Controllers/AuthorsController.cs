using Microsoft.AspNetCore.Mvc;
using Pri.Ee.Client.Models.Authors;
using Pri.Ee.Client.Models.Books;
using Pri.Ee.Client.Services;

namespace Pri.Ee.Client.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var author = await _authorService.GetAll();
            return View(author);
        }

        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorService.GetById(id);
            return View(author);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var success = await _authorService.Create(model);

            if (!success)
            {
                ViewBag.Error = "Create failed";
                return View(model);
                
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetById(id);
            var model = new CreateAuthorViewModel
            {
                Name = author.Name
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateAuthorViewModel model)
        {
            var success = await _authorService.Update(id, model);

            if (!success)
            {
                ViewBag.Error = "Update failed";
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var success = await _authorService.Delete(id);
            if (!success)
            {
                TempData["Error"] = "Delete failed";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

