using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspweb.Models;
using PagedList;

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
        public ActionResult ProductGrid(int? page, string searchString,string id,string sortOrder, decimal amountMin = decimal.MinValue, decimal amountMax = decimal.MaxValue)
        {
            ViewBag.SapTheoGia = sortOrder == "gia" ? "gia_desc" : "gia";
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var all_product = db.tbl_products.Where(p => p.price >= amountMin && p.price <= amountMax);
            if (!String.IsNullOrEmpty(id))
            {
                int iid = Int32.Parse(id);
                ViewBag.id = id;
                all_product = all_product.Where(p => p.category_id == iid);
            }
            switch (sortOrder)
            {
                case "gia":
                    all_product = all_product.OrderBy(s => s.price);
                    break;
                case "gia_desc":
                    all_product = all_product.OrderByDescending(s => s.price);
                    break;
                default:
                    break;
            }
            if(Request.HttpMethod == "POST")
            {
                all_product = all_product.Where(p => p.title.Contains(searchString));
            }
            var all_category = db.tbl_category.Select(p => p).ToList();
            Entity entity = new Entity();
            entity._Products = all_product.OrderBy(p => p.id).ToPagedList(pageNumber, pageSize);
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