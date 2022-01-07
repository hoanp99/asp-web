using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspweb.Models;
using aspweb.DTO;

namespace aspweb.Controllers
{
    public class CartController : Controller
    {
        CosmesticShopDB db = new CosmesticShopDB();
        //Lấy giỏ hàng
        public List<Cart> GetCart()
        {
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart == null)
            {
                //Nếu giở hàng chưa tồn tại thì khởi tạo
                lstCart = new List<Cart>();
                Session["Cart"] = lstCart;
            }
            return lstCart;
        }
        //Thêm hàng vào giỏ
        public ActionResult AddToCart(string iid, string strUrl)
        {
            //Lấy ra Session giỏ hàng
            List<Cart> lstCart = GetCart();
            //Kiểm tra sản phẩm có tồn tại trong giỏ hagnf chưa
            int id = Int32.Parse(iid);

                var sp = lstCart.Find(n => n.iid == id);
                if (sp == null)
                {
                    sp = new Cart(id);
                    lstCart.Add(sp);
                    return Redirect(strUrl);
                }
                else
                {
                    sp.iquantity++;

                    return Redirect(strUrl);
                }
            



        }
        //Tổng số lượng
        private int squantity()
        {
            int squantity = 0;
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart != null)
            {
                squantity = lstCart.Sum(n => n.iquantity);
            }
            return squantity;
        }
        //Tổng tiền
        private int stotal()
        {
            int stotal = 0;
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart != null)
            {
                stotal = lstCart.Sum(n => n.itotal);
            }
            return stotal;
        }
        //tax
        private int tax()
        {

            int stotal = 0;
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart != null)
            {
                stotal = lstCart.Sum(n => n.itotal);
            }
            return stotal * 10 / 100;
        }
        //Tổng phải trả
        private int Tong()
        {

            int stotal = 0;
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart != null)
            {
                stotal = lstCart.Sum(n => n.itotal);
            }
            return stotal * 10 / 100 + stotal;
        }
        //Xây dựng trang giỏ hàng
        public ActionResult Carts()
        {
            List<Cart> lstCart = GetCart();
            if (lstCart.Count() == 0)
            {
                return RedirectToAction("Index", "Client");
            }
            ViewBag.squantity = squantity();
            ViewBag.stotal = stotal();
            ViewBag.tax = tax();
            ViewBag.Tong = Tong();
            return View(lstCart);
        }
        //Xóa giỏ hàng
        public ActionResult RemoveCart(int isp)
        {
            //Lấy giỏ hàng từ session
            List<Cart> lstCart = GetCart();
            //Kiểm tra sản phẩm có trong giỏ hàng chưa
            Cart sp = lstCart.SingleOrDefault(n => n.iid == isp);
            //Nếu tồn tại thì cho xóa iquantity

            if (sp != null)
            {
                lstCart.RemoveAll(n => n.iid == isp);
                return RedirectToAction("Carts");
            }
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "Client");
            }
            return RedirectToAction("Carts");
        }
        //Cập nhật giỏ hàng
        public ActionResult UpdateCart(FormCollection f)
        {
            //Lấy giỏ hàng từ session

            int id_pro = int.Parse(f["id_product"]);
            int quantity = int.Parse(f["quantity"]);
            Cart cart = Session["Cart"] as Cart;
            //Kiểm tra sp đã có trong Session["Cart"]
            // Cart sp = lstCart.SingleOrDefault(n => n.iid == iid);
            //Nếu tồn tại thì sửa số lượng
            cart.Update(id_pro, quantity);

            return RedirectToAction("Carts", "Cart");
        }
        //Xóa tất cả giỏ hàng
        public ActionResult DeleteCart()
        {
            List<Cart> lstcart = GetCart();
            lstcart.Clear();
            return RedirectToAction("Index", "Client");

        }

        [HttpGet]
        public ActionResult Checkout()
        {
            if(Session["Account"] == null || Session["Account"].ToString() == "")
            {
                return RedirectToAction("index", "clientlogin");
            }
            List<Cart> lstCart = GetCart();
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "Client");
            }
            ViewBag.squantity = squantity();
            ViewBag.stotal = stotal();
            ViewBag.tax = tax();
            ViewBag.Tong = Tong();
            return View(lstCart);
            //return View();
        }

        List<Cart> items = new List<Cart>();
        public IEnumerable<Cart> Items
        {

            get { return items; }
        }
        public PartialViewResult BagCart()
        {
            int total_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                total_item = cart.iquantity;
            ViewBag.QuantityCart = total_item;
            return PartialView("BagCart");
        }

        //method checkout
        public ActionResult Checkout2(FormCollection collection)
        {
            //Thêm đơn hàng
            tbl_saleorder _Saleorder = new tbl_saleorder();
            tbl_users _Users = (tbl_users)Session["Account"];
            List<Cart> lstCart = GetCart();
            var name = collection["name"];
            _Saleorder.code = GenerateSaleOrderCode.Code();
            _Saleorder.user_id = _Users.id;
            _Saleorder.total = Tong();
            _Saleorder.created_date = DateTime.Now;
            _Saleorder.customer_name = collection["name1111"];
            _Saleorder.customer_address = collection["address"];
            _Saleorder.cutomer_email = _Users.email;
            _Saleorder.customer_phone = Convert.ToString(_Users.phone);
            db.tbl_saleorder.Add(_Saleorder);
            db.SaveChanges();
            foreach(var item in lstCart)
            {
                tbl_saleorder_products _Saleorder_Products = new tbl_saleorder_products();
                _Saleorder_Products.id = _Saleorder.id;
                _Saleorder_Products.saleorder_id = _Saleorder.id;
                _Saleorder_Products.product_id = item.iid;
                _Saleorder_Products.quality = item.iquantity;
                _Saleorder_Products.created_date = _Saleorder.created_date;
                _Saleorder_Products.status = 0;
                var tempProduct = db.tbl_products.Where(x => x.id == _Saleorder_Products.product_id).FirstOrDefault();
                tempProduct.quantity_left -= item.iquantity;
                db.tbl_saleorder_products.Add(_Saleorder_Products);
            }
            db.SaveChanges();
            Session["Cart"] = null;
            return RedirectToAction("PaymentSuccess", "Cart");
        }

        public ActionResult PaymentSuccess()
        {
            return View();
        }

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

    }
}
