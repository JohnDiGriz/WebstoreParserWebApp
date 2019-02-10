    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Repositories.Interfaces;

namespace ParserWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork db;
        private IProductRepository Products { get { return db.ProductRepository; } }
        private int ProductsOnPage_ = 20;
        public ProductsController(IUnitOfWork unitOfWork) : base()
        {
            db = unitOfWork;
        }
        public ActionResult Index(int? page)
        {
            if (page == null) { page = 1; }
            ViewBag.PageNum = page;
            return View();
        }
        public ActionResult Details(int id)
        {
            var product = Products.Get(id);
            var productsVM = new ViewModels.ProductDetailsViewModel()
            {
                Id = product.Id,
                Name = product.Name
            };
            return View(productsVM);
        }
        public ActionResult GetProduct(int id)
        {
            var responce = new ViewModels.ProductViewModel()
            {
                PriceHistory = new List<ViewModels.PriceViewModel>(),
                Thumbs = new List<ViewModels.PictureViewModel>()
            };
            var product = db.Products.Include(x => x.Prices).Include(x => x.Pictures).First(x => x.Id == id);
            responce.Product = product;
            if (product.Pictures != null)
                responce.Thumbs = product.Pictures.ToList();
            if (product.Prices != null)
                responce.PriceHistory = product.Prices.ToList();
            return Json(responce, JsonRequestBehavior.AllowGet);
        }
        private int LastPage(int objectsCount)
        {
            return objectsCount % ProductsOnPage_ == 0 ? 0 : 1;
        }
        public ActionResult GetProducts(int id)
        {
            ProductsResponce responce = new ProductsResponce() { Products = new List<Models.Product>() };
            responce.PageCount = db.Products.Count() / ProductsOnPage_ + LastPage(db.Products.Count());
            if (responce.PageCount >= id)
            {
                int skippedProducts = (id - 1) * ProductsOnPage_;
                int displayedProducts = Math.Min(20, db.Products.Count() - skippedProducts);
                responce.Products = db.Products.Skip(skippedProducts).Take(skippedProducts).ToList();
            }
            else
                responce.Products = null;
            responce.PageNum = id;

            return Json(responce, JsonRequestBehavior.AllowGet);
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
