    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParserWebApp.Controllers
{
    public class ProductsController : Controller
    {

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
            ProductResponce responce=new ProductResponce();
            using (Models.SiteContext db = new Models.SiteContext())
            {
                var pictures = db.Pictures.ToList();
                var prices = db.Prices.ToList();
                var product = db.Products.Find(id);
                responce.Product = product;
                if (product.Pictures != null)
                    responce.Thumbs = product.Pictures.ToList();
                else
                    responce.Thumbs = new List<Models.Picture>();
                if (product.Prices != null)
                    responce.PriceHistory = product.Prices.ToList();
                else
                    responce.PriceHistory = new List<Models.Price>();
            }
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
            using (Models.SiteContext db = new Models.SiteContext())
            {
                responce.PageCount = db.Products.Count() / 20 + (db.Products.Count() % 20 == 0 ? 0 : 1);
                if (responce.PageCount >= id)
                {
                    responce.Products = db.Products.ToList().GetRange((id - 1) * 20, Math.Min(20, db.Products.Count() - (id - 1) * 20));
                }
                else
                    responce.Products = null;
                responce.PageNum = id;
            }
            return Json(responce, JsonRequestBehavior.AllowGet);
        }
    }
}