using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspweb.Models
{
    public class Entity
    {
        public List<tbl_products> _Products { get; set; }
        public List<tbl_category> _Categories { get; set; }
    }
}