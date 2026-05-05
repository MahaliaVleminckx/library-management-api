using Pri.Ee.Core.Data;
using Pri.Ee.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pri.Ee.Core.Entities;

namespace Pri.Ee.Core.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly ApplicationDbContext _context;
        public LoanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Loan> GetAll()
        {
            return _context.Loans.ToList();
        }

        public Loan? GetById(int id)
        {
            return _context.Loans.Find(id);
        }

        public void Add(Loan loan)
        {
            _context.Loans.Add(loan);
            _context.SaveChanges();
        }
        public void Update(Loan loan)
        {
            _context.Loans.Update(loan);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var loan = _context.Loans.Find(id);
            if (loan != null)
            {
                _context.Loans.Remove(loan);
                _context.SaveChanges();
            }
        }


    }
}
