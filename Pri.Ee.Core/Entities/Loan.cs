using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ee.Core.Entities
{
    public class Loan
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate {  get; set; }
    }
}
