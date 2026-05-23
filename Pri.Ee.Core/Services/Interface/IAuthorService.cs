using Pri.Ee.Core.DTOs;
using Pri.Ee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ee.Core.Services.Interface
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAllAsync();
        Task<AuthorDto?> GetByIdAsync(int id);
        Task <AuthorDto> CreateAsync(AuthorCreateDto dto);
        Task<bool> UpdateAsync(int id, AuthorUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<BookDto>> GetBooksByAuthorAsync (int authorId);
    }
}
