using Microsoft.AspNetCore.Mvc;
using Pri.Ee.Client.Models.Categories;
using Pri.Ee.Client.Models.Loans;
using Pri.Ee.Client.Services;

namespace Pri.Ee.Client.Controllers
{
    public class LoansController : Controller
    {
        private readonly LoanService _loanService;

        public LoansController(LoanService loansService)
        {
            _loanService = loansService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _loanService.GetAll();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var categories = await _loanService.GetById(id);
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateLoanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var success = await _loanService.Create(model);

            if (!success)
            {
                ViewBag.Error = "Could not create loan";
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var loan = await _loanService.GetById(id);
            var model = new CreateLoanViewModel
            {
               BookId = loan.BookId,
               UserId = loan.UserId,
               LoanDate = loan.LoanDate,
               ReturnDate = loan.ReturnDate,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateLoanViewModel model)
        {
            var success = await _loanService.Update(id, model);

            if (!success)
            {
                ViewBag.Error = "Update failed";
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var success = await _loanService.Delete(id);
            if (!success)
            {
                TempData["Error"] = "Delete failed";
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
