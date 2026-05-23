using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pri.Ee.Core.DTOs;
using Pri.Ee.Core.Entities;

namespace Pri.Ee.Core.Services.Interface
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllAsync(string? search, int? authorId, int? categoryId);
        Task <BookDto?> GetByIdAsync(int id);
        Task<BookDto> CreateAsync(BookCreateDto dto);
        Task <bool> UpdateAsync (int id, BookUpdateDto dto);
        Task <bool> DeleteAsync (int id);

    }
}
