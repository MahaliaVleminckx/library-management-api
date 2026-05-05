using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pri.Ee.Core.Entities;

namespace Pri.Ee.Core.Services.Interface
{
    public interface IBookService
    {
        List<Book> GetAll();
        Book? GetById(int id);
        void Add(Book book);
        void Update(Book book);
        void Delete(int id); 
    }
}
