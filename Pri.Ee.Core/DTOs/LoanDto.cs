using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ee.Core.DTOs
{
    public class LoanDto
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
