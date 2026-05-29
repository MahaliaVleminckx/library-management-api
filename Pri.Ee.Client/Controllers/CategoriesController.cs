using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pri.Ee.Client.Models.Books;
using Pri.Ee.Client.Models.Categories;
using Pri.Ee.Client.Services;

namespace Pri.Ee.Client.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoriesService)
        {
            _categoryService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var categories = await _categoryService.GetById(id);
            return View(categories);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var success = await _categoryService.Create(model);

            if (!success)
            {
                ViewBag.Error = "Could not create category";
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetById(id);
            var model = new CreateCategoryViewModel
            {
                Name = category.Name
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var success = await _categoryService.Update(id, model);

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
            var success = await _categoryService.Delete(id);
            if (!success)
            {
                TempData["Error"] = "Delete failed";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
