using Pri.Ee.Core.Data;
using Pri.Ee.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pri.Ee.Core.Entities;
using Pri.Ee.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Pri.Ee.Core.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly ApplicationDbContext _context;
        public LoanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoanDto>> GetAllAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Select(l => new LoanDto
                {
                    Id = l.Id,
                    BookTitle = l.Book.Title,
                    UserId = l.UserId,
                    LoanDate = l.LoanDate,
                    ReturnDate = l.ReturnDate
                })
                .ToListAsync();
        }

        public async Task<LoanDto?> GetByIdAsync(int id)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Where(l => l.Id == id)
                .Select(l => new LoanDto
                {
                    Id = l.Id,
                    BookTitle = l.Book.Title,
                    UserId = l.UserId,
                    LoanDate = l.LoanDate,
                    ReturnDate = l.ReturnDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task<LoanDto> CreateAsync(LoanCreateDto dto)
        {
            var book = await _context.Books.FindAsync(dto.BookId);

            if (book == null)
            {
                return null;
            }

            var loan = new Loan
            {
                BookId = dto.BookId,
                UserId = dto.UserId,
                LoanDate = DateTime.Now
            };

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            
            return new LoanDto
            {
                Id = loan.Id,
                BookTitle = book.Title,
                UserId = loan.UserId,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate
            };
        }

        public async Task<bool> UpdateAsync (int id, LoanUpdateDto dto)
        {
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
            {
                return false;
            }

            loan.ReturnDate = dto.ReturnDate;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var loan = await _context.Loans.FindAsync (id);

            if (loan == null )
            {
                return false;
            }
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
