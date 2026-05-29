using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pri.Ee.Client.Models.Categories;
using Pri.Ee.Client.Models.Loans;
using Pri.Ee.Client.Services;

namespace Pri.Ee.Client.Controllers
{
    public class LoansController : Controller
    {
        private readonly LoanService _loanService;

        public LoansController(LoanService loanService)
        {
            _loanService = loanService;
        }

        public async Task<IActionResult> Index()
        {
            var loans = await _loanService.GetAll();
            return View(loans);
        }

        public async Task<IActionResult> Details(int id)
        {
            var loan = await _loanService.GetById(id);

            if (loan == null)
                return NotFound();

            return View(loan);
        }

        public IActionResult Create()
        {
            return View(new CreateLoanViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLoanViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

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

            if (loan == null)
                return NotFound();

            var model = new UpdateLoanViewModel
            {
                ReturnDate = loan.ReturnDate,
                LoanDate = loan.LoanDate
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateLoanViewModel model)
        {
            
            if (model.ReturnDate.HasValue && model.ReturnDate < model.LoanDate)
            {
                ModelState.AddModelError("ReturnDate", "Return date cannot be before loan date");
            }

            if (!ModelState.IsValid)
                return View(model);

            
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
                TempData["Error"] = "Delete failed";

            return RedirectToAction(nameof(Index));
        }
    }

}
