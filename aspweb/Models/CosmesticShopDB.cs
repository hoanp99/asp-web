using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace aspweb.Models
{
    public partial class CosmesticShopDB : DbContext
    {
        public CosmesticShopDB()
            : base("name=CosmesticShopDB")
        {
        }

        public virtual DbSet<tbl_category> tbl_category { get; set; }
        public virtual DbSet<tbl_products> tbl_products { get; set; }
        public virtual DbSet<tbl_roles> tbl_roles { get; set; }
        public virtual DbSet<tbl_saleorder> tbl_saleorder { get; set; }
        public virtual DbSet<tbl_saleorder_products> tbl_saleorder_products { get; set; }
        public virtual DbSet<tbl_users> tbl_users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_category>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_category>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_category>()
                .HasMany(e => e.tbl_products)
                .WithOptional(e => e.tbl_category)
                .HasForeignKey(e => e.category_id);

            modelBuilder.Entity<tbl_products>()
                .Property(e => e.price)
                .HasPrecision(13, 2);

            modelBuilder.Entity<tbl_products>()
                .Property(e => e.price_sale)
                .HasPrecision(13, 2);

            modelBuilder.Entity<tbl_products>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_products>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_products>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_products>()
                .HasMany(e => e.tbl_saleorder_products)
                .WithRequired(e => e.tbl_products)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_roles>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_roles>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_roles>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_roles>()
                .HasMany(e => e.tbl_users)
                .WithRequired(e => e.tbl_roles)
                .HasForeignKey(e => e.role_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_saleorder>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_saleorder>()
                .Property(e => e.total)
                .HasPrecision(13, 2);

            modelBuilder.Entity<tbl_saleorder>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_saleorder>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_saleorder>()
                .Property(e => e.customer_name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_saleorder>()
                .Property(e => e.customer_address)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_saleorder>()
                .Property(e => e.customer_phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_saleorder>()
                .Property(e => e.cutomer_email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_saleorder>()
                .HasMany(e => e.tbl_saleorder_products)
                .WithRequired(e => e.tbl_saleorder)
                .HasForeignKey(e => e.saleorder_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_saleorder_products>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_saleorder_products>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_users>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_users>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_users>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_users>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_users>()
                .Property(e => e.updated_by)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_users>()
                .HasMany(e => e.tbl_saleorder)
                .WithOptional(e => e.tbl_users)
                .HasForeignKey(e => e.user_id);
        }
    }
}
