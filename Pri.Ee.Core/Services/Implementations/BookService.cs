using Pri.Ee.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pri.Ee.Core.Data;
using Pri.Ee.Core.Entities;

namespace Pri.Ee.Core.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book? GetById (int id)
        {
            return _context.Books.Find(id);
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update (Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
