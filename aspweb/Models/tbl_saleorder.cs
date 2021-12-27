namespace aspweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_saleorder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_saleorder()
        {
            tbl_saleorder_products = new HashSet<tbl_saleorder_products>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string code { get; set; }

        public int? user_id { get; set; }

        public decimal? total { get; set; }

        public DateTime? created_date { get; set; }

        public DateTime? updated_date { get; set; }

        public int? created_by { get; set; }

        public int? updated_by { get; set; }

        public int? status { get; set; }

        [StringLength(100)]
        public string customer_name { get; set; }

        [StringLength(100)]
        public string customer_address { get; set; }

        [StringLength(100)]
        public string customer_phone { get; set; }

        [StringLength(100)]
        public string cutomer_email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_saleorder_products> tbl_saleorder_products { get; set; }
    }
}
