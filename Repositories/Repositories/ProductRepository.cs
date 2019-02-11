using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace ParserWebApp.DAL.Repositories
{
    class ProductRepository : Repository<Models.Product>, Interfaces.IProductRepository
    {
        public ProductRepository(Models.SiteContext context) : base(context) { }
        public IEnumerable<Models.Product> GetPage(int pageNumber, int productsCount)
        {
            int skippedProducts = (pageNumber - 1) * productsCount;
            int displayedProducts = Math.Min(productsCount, SiteContext.Products.Count() - skippedProducts);
            return SiteContext.Products.Skip(skippedProducts).Take(displayedProducts).ToList();
        }
        public Models.Product GetProduct(int id)
        {
            return SiteContext.Products.Include(x => x.Prices).Include(x => x.Pictures).First(x => x.Id == id);
        }
        public int GetPageCount(int productsOnPage)
        {
            return SiteContext.Products.Count() / productsOnPage + (SiteContext.Products.Count()%productsOnPage==0?0:1); 
        }
        private Models.SiteContext SiteContext { get { return Context_ as Models.SiteContext; } }
    }
}
