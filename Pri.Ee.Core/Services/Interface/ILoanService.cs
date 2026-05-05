using Pri.Ee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ee.Core.Services.Interface
{
    public interface ILoanService
    {
        List<Loan> GetAll ();
        Loan? GetById(int id);
        void Add (Loan loan);
        void Update (Loan loan);
        void Delete (int id);
    }
}
