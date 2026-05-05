using Pri.Ee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ee.Core.Services.Interface
{
    public interface IAuthorService
    {
        List<Author> GetAll();
        Author? GetById (int id);
    }
}
