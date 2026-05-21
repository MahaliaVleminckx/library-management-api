using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ee.Core.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        //Relaties
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Loan> Loans { get; set; }
    }
}
