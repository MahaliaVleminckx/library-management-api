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
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorDto>> GetAllAsync()
        {
            return await _context.Authors
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                })
                .ToListAsync();
        }

        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            return await _context.Authors
                .Where(a => a.Id == id )
                .Select (a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                })
                .FirstOrDefaultAsync();
        }
        
        public async Task<AuthorDto> CreateAsync(AuthorCreateDto dto)
        {
            var author = new Author
            {
                Name = dto.Name
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name
            };
        }

        public async Task<bool> UpdateAsync (int id, AuthorUpdateDto dto)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return false;
            }
            author.Name = dto.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync (id);
            if (author == null)
            {
                return false;
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BookDto>> GetBooksByAuthorAsync(int authorId)
        {
            var books = await _context.Books
                .Where(b=> b.AuthorId == authorId)
                .Include(b=>b.Author)
                .ToListAsync();

            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                AuthorName = b.Author.Name,
                AuthorId = b.AuthorId,
                CategoryId = b.CategoryId
            }).ToList();
        }
    }
}
