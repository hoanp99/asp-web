using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspweb.Models
{
    public class Order
    {
        public tbl_saleorder _Saleorder { get; set; }
        public List<tbl_saleorder_products> _Products { get; set; }
    }
}