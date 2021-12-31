namespace aspweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_roles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_roles()
        {
            tbl_users = new HashSet<tbl_users>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string name { get; set; }

        [Required]
        [StringLength(45)]
        public string description { get; set; }

        public DateTime? created_date { get; set; }

        public DateTime? updated_date { get; set; }

        [StringLength(50)]
        public string created_by { get; set; }

        [StringLength(50)]
        public string updated_by { get; set; }

        public byte? status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_users> tbl_users { get; set; }
    }
}
