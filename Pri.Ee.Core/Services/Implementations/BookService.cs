using Pri.Ee.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pri.Ee.Core.Data;
using Pri.Ee.Core.Entities;
using Pri.Ee.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Pri.Ee.Core.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task <List<BookDto>> GetAllAsync()
        {
            return await _context.Books
                .Include (b=> b.Author)
                .Select(b=> new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    AuthorName = b.Author.Name
                })
                .ToListAsync();
        }

        public async Task<BookDto?> GetById(int id)
        {
            var book = await _context.Books
                .Include (b=> b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return null;
            }

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorName = book.Author.Name
            };
        }

        public async Task<BookDto> CreateAsync (BookCreateDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Description = dto.Description,
                AuthorId = dto.AuthorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            
            var author = await _context.Authors.FindAsync(book.AuthorId);

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorName = author?.Name ?? ""
            };
        }

        public async Task<bool> UpdateAsync (int id, BookUpdateDto dto)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return false;
            }

            book.Title = dto.Title;
            book.Description = dto.Description;
            book.AuthorId = dto.AuthorId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
