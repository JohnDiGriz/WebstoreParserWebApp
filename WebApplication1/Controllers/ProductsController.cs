    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ParserWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Models.SiteContext db;

        public ProductsController() : base()
        {
            db = new Models.SiteContext();
        }
        public ActionResult Index(int? page)
        {
            if (page == null) { page = 1; }
            ViewBag.PageNum = page;
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Products");
            using (Models.SiteContext db = new Models.SiteContext())
            {
                Models.Product product = db.Products.Find(id);
                ViewBag.Title=product.Name;
                ViewBag.Id = product.Id;
            }
            return View();
        }
        class ProductResponce
        {
            public Models.Product Product;
            public List<Models.Picture> Thumbs;
            public List<Models.Price> PriceHistory;
        }
        public ActionResult GetProduct(int id)
        {
            ProductResponce responce = new ProductResponce()
            {
                PriceHistory = new List<Models.Price>(),
                Thumbs = new List<Models.Picture>()
            };
            var product = db.Products.Include(x => x.Prices).Include(x => x.Pictures).First(x => x.Id == id);
            responce.Product = product;
            if (product.Pictures != null)
                responce.Thumbs = product.Pictures.ToList();
            if (product.Prices != null)
                responce.PriceHistory = product.Prices.ToList();
            return Json(responce, JsonRequestBehavior.AllowGet);
        }
        class ProductsResponce
        {
            public List<Models.Product> Products { get; set; }
            public int PageCount;
            public int PageNum;
        }
        public ActionResult GetProducts(int id)
        {
            ProductsResponce responce = new ProductsResponce() { Products = new List<Models.Product>() };
            responce.PageCount = db.Products.Count() / 20 + (db.Products.Count() % 20 == 0 ? 0 : 1);
            if (responce.PageCount >= id)
            {
                responce.Products = db.Products.Skip((id - 1) * 20).Take(Math.Min(20, db.Products.Count() - (id - 1) * 20)).ToList();
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
