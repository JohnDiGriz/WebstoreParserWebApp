using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Models.Product>
    {
        IEnumerable<Models.Product> GetPage(int pageNumber, int productsCount);
        Models.Product GetProduct(int id);
        int GetPageCount(int productsOnPage);
    }
}
