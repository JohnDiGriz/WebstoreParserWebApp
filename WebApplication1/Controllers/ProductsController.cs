    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Repositories.Interfaces;
using AutoMapper;

namespace ParserWebApp.Controllers
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
            var model = new ViewModels.ProductIndexViewModel();
            model.PageNum = (int)page;
            model.Name = "Home Page";
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var product = Products.Get(id);
            if (product != null)
            {
                var model = new ViewModels.ProductDetailsViewModel();
                Mapper_.Map(product, model);
                return View(model);
            }
            return View("Error");
        }
        public ActionResult GetProduct(int id)
        {
            var product = Products.GetProduct(id);
            if (product != null)
            {
                var model = new ViewModels.ProductViewModel();
                Mapper_.Map(product, model);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return View("Error");
        }
        private int LastPage(int objectsCount)
        {
            return objectsCount % ProductsOnPage_ == 0 ? 0 : 1;
        }
        public ActionResult GetProducts(int id)
        {
            var products = Products.GetPage(id, ProductsOnPage_);
            if(products!=null)
            {
                var model = new ViewModels.ProductListViewModel();
                Mapper_.Map(products, model);
                model.PageCount = Products.GetPageCount(ProductsOnPage_);
                model.PageNum = id;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return View("Error");
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
