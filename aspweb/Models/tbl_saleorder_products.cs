namespace aspweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_saleorder_products
    {
        public int id { get; set; }

        public int saleorder_id { get; set; }

        public int product_id { get; set; }

        public int quality { get; set; }

        public DateTime? created_date { get; set; }

        public DateTime? updated_date { get; set; }

        [StringLength(50)]
        public string created_by { get; set; }

        [StringLength(50)]
        public string updated_by { get; set; }

        public byte? status { get; set; }

        public virtual tbl_products tbl_products { get; set; }

        public virtual tbl_saleorder tbl_saleorder { get; set; }
    }
}
