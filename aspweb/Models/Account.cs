using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspweb.Models
{
    public class Account
    {
        public tbl_users users { get; set; }
        public tbl_roles roles { get; set; }
    }
}