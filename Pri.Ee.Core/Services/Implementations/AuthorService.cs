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
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Author> GetAll()
        {
            return _context.Authors.ToList();
        }

        public Author? GetById(int id)
        {
            return _context.Authors.Find(id);
        }
    }
}
