using Pri.Ee.Core.DTOs;
using Pri.Ee.Core.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pri.Ee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;
        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _loanService.GetAllAsync();
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var loan = await _loanService.GetByIdAsync(id);

            if(loan == null)
            {
                return NotFound();
            }

            return Ok(loan);
        }

        [HttpPost]
        public async Task <IActionResult> Create(LoanCreateDto dto)
        {
            var created = await _loanService.CreateAsync(dto);

            if(created == null)
            {
                return BadRequest("Book does not exist.");
            }

            return CreatedAtAction
                (
                nameof(GetById),
                new { id = created.Id },
                created
                );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LoanUpdateDto dto)
        {
            var success = await _loanService.UpdateAsync(id, dto);

            if(!success)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var success = await _loanService.DeleteAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
        
    }
}
