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
    public class ClientLoginController : Controller
    {
        CosmesticShopDB db = new CosmesticShopDB();
        // GET: ClientLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {

            using (var db = new CosmesticShopDB())
            {
                var tendn = collection["TenDN"];
                var matkhau = collection["Matkhau"];
                matkhau = PasswordEncode.Encode(matkhau);
                if (String.IsNullOrEmpty(tendn)) 
                { ViewBag["Loi1"] = "Phải nhập tên đăng nhập"; }
                else if (String.IsNullOrEmpty(matkhau))
                { ViewBag["Loi2"] = "Mật khẩu không được để trống"; }
                else
                {
                    tbl_users user = db.tbl_users.SingleOrDefault(n => n.username == tendn && n.password == matkhau);
                    if(user != null)
                    {
                        ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                        Session["Account"] = user;
                        return RedirectToAction("Checkout", "Cart");
                    }
                    else
                    {
                        ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                    }
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "ClientLogin");
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, tbl_users user)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matnhaplai"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            if (String.IsNullOrEmpty(tendn)) 
            { ViewData["Loi1"] = "Tên đăng nhập không được để trống"; }
            else if(String.IsNullOrEmpty(matkhau))
            { ViewData["Loi2"] = "Mật khẩu không được để trống"; }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi3"] = "Mật khẩu nhập lại không được để trống";
            }
            else if (matkhaunhaplai != matkhau)
            {
                ViewData["Loi3"] = "Mật khẩu nhập lại không đúng";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi4"] = "Email không được để trống";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi5"] = "Điện thoại không được để trống";
            }
            else{
                user.username = tendn;
                user.password = matkhau;
                user.email = email;
                user.phone = Int32.Parse(dienthoai);
                user.created_date = DateTime.Now;
                user.role_id = 2;
                db.tbl_users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.DangKy();
        }
    }
}