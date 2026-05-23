using Pri.Ee.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ee.Core.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task <CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryCreateDto dto);
        Task<bool> UpdateAsync(int id, CategoryUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<BookDto>> GetBooksByCategoryAsync(int categoryId);
    }
}
