using Pri.Ee.Core.Entities;
using Pri.Ee.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ee.Core.Services.Interface
{
    public interface ILoanService
    {
       Task  <List<LoanDto>> GetAllAsync ();
        Task <LoanDto?> GetByIdAsync(int id);
        Task<LoanDto?> CreateAsync(LoanCreateDto dto);
        Task<bool> UpdateAsync (int id, LoanUpdateDto dto);
        Task<bool> DeleteAsync (int id);
    }
}
