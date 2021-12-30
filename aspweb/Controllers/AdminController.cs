﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using aspweb.Models;
using PagedList;
using aspweb.DTO;

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

        #region Categories
        public ActionResult Categories(int? page)
        {
            var categories = db.tbl_category.Select(p => p);
            categories = categories.OrderBy(s => s.id);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(categories.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory([Bind(Include = "name,description")] tbl_category category)
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

        public ActionResult DetailCategory(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category category = db.tbl_category.Find(Int32.Parse(id));
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        public ActionResult UpdateCategory(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category category = db.tbl_category.Find(Int32.Parse(id));
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCategory([Bind(Include = "id,name,description,created_date,created_by")] tbl_category category)
        {
            DateTime now = DateTime.Now;
            category.updated_date = now;
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Categories", "Admin");
            }
            return View(category);
        }

        public ActionResult DeleteCategory(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category category = db.tbl_category.Find(Int32.Parse(id));
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryConfirmed(string id)
        {
            tbl_category category = db.tbl_category.Find(Int32.Parse(id));
            db.tbl_category.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Categories", "Admin");
        }
        #endregion

        #region Products
        public ActionResult Products(int? page)
        {
            var products = db.tbl_products.Select(p => p);
            products = products.OrderBy(s => s.id);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CreateProduct()
        {
            ViewBag.category_id = new SelectList(db.tbl_category, "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct([Bind(Include = "title,price,price_sale,short_description,detail_description,quantity,image,category_id")] tbl_products product)
        {
            DateTime now = DateTime.Now;
            if (ModelState.IsValid)
            {
                product.quantity_left = product.quantity;
                product.created_date = now;
                product.image = "";
                var f = Request.Files["ImageFile"];
                if (f != null)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/Resources/Images/" + FileName);
                    f.SaveAs(UploadPath);
                    product.image = FileName;
                }
                db.tbl_products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Products", "Admin");
            }
            ViewBag.category_id = new SelectList(db.tbl_category, "id", "name", product.category_id);
            return View(product);
        }

        public ActionResult UpdateProduct(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.category_id = new SelectList(db.tbl_category, "id", "name");
            tbl_products product = db.tbl_products.Find(Int32.Parse(id));
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct([Bind(Include = "id,title,price,price_sale,short_description,detail_description,quantity,image,category_id,created_date,created_by")] tbl_products product)
        {
            DateTime now = DateTime.Now;
            if (ModelState.IsValid)
            {
                product.quantity_left = product.quantity;
                product.updated_date = now;
                var temp = "";
                temp = product.image;
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/Resources/Images/" + FileName);
                    f.SaveAs(UploadPath);
                    product.image = FileName;           
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Products", "Admin");
            }
            ViewBag.category_id = new SelectList(db.tbl_category, "id", "name", product.category_id);
            return View(product);
        }

        public ActionResult DetailProduct(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.category_id = new SelectList(db.tbl_category, "id", "name");
            tbl_products product = db.tbl_products.Find(Int32.Parse(id));
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult DeleteProduct(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.category_id = new SelectList(db.tbl_category, "id", "name");
            tbl_products product = db.tbl_products.Find(Int32.Parse(id));
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProductConfirmed(string id)
        {
            tbl_products product = db.tbl_products.Find(Int32.Parse(id));
            db.tbl_products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Products", "Admin");
        }
        #endregion

        #region SaleOrder
        public ActionResult SaleOrders(int? page)
        {
            var saleorders = db.tbl_saleorder.Select(p => p);
            saleorders = saleorders.OrderBy(s => s.id);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(saleorders.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UpdateSaleOrder()
        {
            return View();
        }

        public ActionResult DeleteSaleOrder()
        {
            return View();
        }

        public ActionResult DetailSaleOrder(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = new Order();
            var saleorder = db.tbl_saleorder.Find(id);
            var orderlist = db.tbl_saleorder_products.Select(p => p).Where(p => p.saleorder_id.Equals(id));
            order._Saleorder = saleorder;
            order._Products = orderlist.ToList();
            if (saleorder == null)
            {
                return HttpNotFound();
            }
            return View(order);
            
        }
        #endregion

        #region Accounts
        public ActionResult Accounts(int? page)
        {
            var accounts = from a in db.tbl_users
                           join b in db.tbl_roles on a.role_id equals b.id
                           select new Account { roles = b, users = a };



            accounts = accounts.OrderBy(p => p.users.id);
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(accounts.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CreateAccount()
        {
            ViewBag.role_id = new SelectList(db.tbl_roles, "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount([Bind(Include = "username,password,email,role_id,phone")] tbl_users user)
        {
            DateTime now = DateTime.Now;

            user.created_date = now;
            if (ModelState.IsValid)
            {
                user.password = PasswordEncode.Encode(user.password);
                db.tbl_users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Accounts", "Admin");
            }
            ViewBag.role_id = new SelectList(db.tbl_roles, "id", "name", user.role_id);
            return View(user);
        }

        public ActionResult UpdateAccount(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.role_id = new SelectList(db.tbl_roles, "id", "name");
            tbl_users user = db.tbl_users.Find(Int32.Parse(id));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAccount([Bind(Include = "id,username,password,email,role_id,phone,created_date,created_by")] tbl_users user)
        {
            DateTime now = DateTime.Now;
            user.updated_date = now;
            if (ModelState.IsValid)
            {
                user.password = PasswordEncode.Encode(user.password);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Accounts", "Admin");
            }
            ViewBag.role_id = new SelectList(db.tbl_roles, "id", "name", user.role_id);
            return View(user);
        }

        public ActionResult DeleteAccount(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_users user = db.tbl_users.Find(Int32.Parse(id));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("DeleteAccount")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccountConfirmed(string id)
        {
            tbl_users user = db.tbl_users.Find(Int32.Parse(id));
            db.tbl_users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Accounts", "Admin");
        }


        public ActionResult DetailAccount(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_users user = db.tbl_users.Find(Int32.Parse(id));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        #endregion
    }
}