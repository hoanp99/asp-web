namespace aspweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_products()
        {
            tbl_saleorder_products = new HashSet<tbl_saleorder_products>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(1000)]
        public string title { get; set; }

        public decimal price { get; set; }

        public decimal? price_sale { get; set; }

        [Required]
        [StringLength(3000)]
        public string short_description { get; set; }

        [Required]
        [StringLength(3000)]
        public string detail_description { get; set; }

        public int quantity { get; set; }

        public int quantity_left { get; set; }

        [Column(TypeName = "text")]
        public string image { get; set; }

        public int? category_id { get; set; }

        public DateTime? created_date { get; set; }

        public DateTime? updated_date { get; set; }

        [StringLength(50)]
        public string created_by { get; set; }

        [StringLength(50)]
        public string updated_by { get; set; }

        public byte? status { get; set; }

        public byte? is_hot { get; set; }

        public virtual tbl_category tbl_category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_saleorder_products> tbl_saleorder_products { get; set; }
    }
}
