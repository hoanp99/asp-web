namespace aspweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_users()
        {
            tbl_saleorder = new HashSet<tbl_saleorder>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string username { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }

        [Required]
        [StringLength(45)]
        public string email { get; set; }

        public int? phone { get; set; }

        public DateTime? created_date { get; set; }

        public DateTime? updated_date { get; set; }

        [StringLength(50)]
        public string created_by { get; set; }

        [StringLength(50)]
        public string updated_by { get; set; }

        public byte? status { get; set; }

        public int role_id { get; set; }

        public virtual tbl_roles tbl_roles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_saleorder> tbl_saleorder { get; set; }
    }
}
