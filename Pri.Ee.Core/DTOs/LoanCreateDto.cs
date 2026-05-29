using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ee.Core.DTOs
{
    public class LoanCreateDto
    {
        public int BookId { get; set; }

        public string UserId { get; set; } = string.Empty;

    }
}
