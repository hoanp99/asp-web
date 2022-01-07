using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspweb.Models
{
    public class Cart
    {
        CosmesticShopDB db = new CosmesticShopDB();
        public int iid { get; set; }
        public string stitle { get; set; }
        public string simage { get; set; }
        public decimal? dprice_sale { get; set; }

        public int iquantity { get; set; }
        public int itotal
        {
            get { return (int)(iquantity * dprice_sale); }
        }
        //Khởi tạo giỏ hàng
        public Cart(int id)
        {
            iid = id;
            tbl_products product = db.tbl_products.Single(n => n.id == iid);
            stitle = product.title;
            simage = product.image;
            dprice_sale = product.price;
            iquantity = 1;

        }
        public void Update(int id, int quantity)
        {
            iid = id;
            tbl_products product = db.tbl_products.Single(n => n.id == iid);
            if(product != null)
            {
                product.quantity = quantity;
            }
        }
    }
}