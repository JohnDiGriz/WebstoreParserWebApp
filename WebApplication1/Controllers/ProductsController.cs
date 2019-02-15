    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ParserWebApp.DAL.Interfaces;
using AutoMapper;

namespace ParserWebApp.WEB.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork db;
        private readonly IMapper Mapper_;
        private IProductRepository Products { get { return db.ProductRepository; } }
        private int ProductsOnPage_ = 20;
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper) : base()
        {
            db = unitOfWork;
            Mapper_ = mapper;
        }
        public ActionResult Index(int? page)
        {
            if (page == null) { page = 1; }
            var products = Products.GetPage((int)page, ProductsOnPage_);
            if (products != null)
            {
                var model = new ViewModels.ProductListViewModel();
                Mapper_.Map(products, model.Products);
                model.PageCount = Products.GetPageCount(ProductsOnPage_);
                model.PageNum = (int)page;
                model.Name = "Home Page";
                return View(model);
            }
            return View("Error");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                return View("Error");
            var product = Products.GetProduct((int)id);
            if (product != null)
            {
                var model = new ViewModels.ProductViewModel();
                Mapper_.Map(product, model);
                return View(model);
            }
            return View("Error");
        }
        private int LastPage(int objectsCount)
        {
            return objectsCount % ProductsOnPage_ == 0 ? 0 : 1;
        }
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
