using Microsoft.EntityFrameworkCore;
using Pri.Ee.Core.Data;
using Pri.Ee.Core.DTOs;
using Pri.Ee.Core.Services.Interface;
using Pri.Ee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ee.Core.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Where(c => c.Id == id)
                .Select (c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CategoryDto> CreateAsync (CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            category.Name = dto.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync (int id)
        {
            var category = await _context.Categories.FindAsync (id);
              

            if (category == null)
            {
                return false;
            }

            var hasBooks = await _context.Books.AnyAsync(b=> b.CategoryId == id);

            if (hasBooks)
            {
                return false;
            }



            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
