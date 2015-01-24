using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Configuration
{
    public class ProductsConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductsConfiguration()
        {
            this.HasMany(e => e.RelatedProducts)
              .WithRequired(e => e.Product)
              .HasForeignKey(e => e.IdProduct)
              .WillCascadeOnDelete(true);


            HasMany(e => e.CategoryProducts)
            .WithRequired(e => e.Product)
            .HasForeignKey(e => e.IdProduct)
            .WillCascadeOnDelete(true);
        }
    }
}