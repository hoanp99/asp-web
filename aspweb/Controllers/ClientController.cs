using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspweb.Models;

namespace aspweb.Controllers
{
    public class ClientController : Controller
    {
        CosmesticShopDB db = new CosmesticShopDB();
        // GET: Client
        public ActionResult Index()
        {
            var new_products = db.tbl_products.OrderByDescending(a => a.price).Take(4).ToList();
            return View(new_products);
        }
        public ActionResult ProductGrid()
        {
            var all_product = db.tbl_products.Select(p => p).ToList();
            var all_category = db.tbl_category.Select(p => p).ToList();
            Entity entity = new Entity();
            entity._Products = all_product;
            entity._Categories = all_category;
            return View(entity);
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult BlogDetail()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult ProductDetail(int id)
        {
            var product = from p in db.tbl_products
                          where p.id == id
                          select p;
            return View(product.Single());
        }
    }
}