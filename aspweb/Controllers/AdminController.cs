using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using aspweb.Models;
using PagedList;

namespace aspweb.Controllers
{
    public class AdminController : Controller
    {
        private CosmesticShopDB db = new CosmesticShopDB();
        // GET: Admin
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Categories(int? page)
        {
            var categories = db.tbl_category.Select(p => p);
            categories = categories.OrderBy(s => s.id);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(categories.ToPagedList(pageNumber, pageSize));       
        }

        public ActionResult CreateCategories()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategories([Bind(Include = "id,name,description")] tbl_category category)
        {
            DateTime now = DateTime.Now;
            category.created_date = now;
            if (ModelState.IsValid)
            {
                db.tbl_category.Add(category);
                db.SaveChanges();
                return RedirectToAction("Categories", "Admin");
            }
            return View(category);
        }

        public ActionResult DeleteCategories(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category category = db.tbl_category.Find(Int32.Parse(id));
            if(category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("DeleteCategories")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoriesConfirmed(string id)
        {
            tbl_category category = db.tbl_category.Find(Int32.Parse(id));
            db.tbl_category.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Categories", "Admin");
        }


        public ActionResult Products()
        {
            return View();
        }

        public ActionResult SaleOrders()
        {
            return View();
        }

        public ActionResult Accounts()
        {
            return View();
        }
    }
}