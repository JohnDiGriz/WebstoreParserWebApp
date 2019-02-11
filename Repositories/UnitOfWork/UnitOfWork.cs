using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserWebApp.DAL.UnitOfWork
{
    public class UnitOfWork : Interfaces.IUnitOfWork
    {
        private readonly Models.SiteContext Context_;
        public Interfaces.IProductRepository ProductRepository { get; private set; }
        public UnitOfWork(Models.SiteContext context)
        {
            Context_ = context;
            ProductRepository = new Repositories.ProductRepository(context);
        }
        public int Save()
        {
            return Context_.SaveChanges();
        }
        public void Dispose()
        {
            Context_.Dispose();
        }
    }
}
