using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using aspweb.Models;
using aspweb.DTO;
using System.Web.Security;

namespace aspweb.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home","Admin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(tbl_users model)
        {
            
            using (var db = new CosmesticShopDB())
            {
                model.password = PasswordEncode.Encode(model.password);
                bool isValid = db.tbl_users.Any(x => x.username == model.username && x.password == model.password);
                var role = db.tbl_users.Where(x => x.username == model.username && x.password == model.password).FirstOrDefault().role_id;
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.username, false);                   
                    if (role != 1)
                    {
                        return RedirectToAction("Index", "Client");
                    }
                    else
                    {
                        return RedirectToAction("Home", "Admin");
                    }
                }
                else
                {
                    ViewBag.Error = "Sai tên tài khoản hoặc mật khẩu";
                }
            }
                return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "AdminLogin");
        }
    }
}