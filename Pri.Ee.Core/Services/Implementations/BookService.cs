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
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Pri.Ee.Core.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task <List<BookDto>> GetAllAsync(string? search, int? authorId,int? categoryId)
        {
            var query = _context.Books
                .Include(b => b.Author)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(b => b.Title.Contains(search));
            }
            if (authorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == authorId);
            }
            if (categoryId.HasValue)
            {
                query = query.Where(b=> b.CategoryId == categoryId);
            }

            return await query.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                AuthorName = b.Author.Name
            }).ToListAsync();
        }

        public async Task<BookDto?> GetByIdAsync(int id)
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
