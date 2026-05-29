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

        public int BookId { get; set; }
        public string? BookTitle { get; set; }

        public string UserId { get; set; } = string.Empty;
        public string? UserName { get; set; }

        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
