using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Configuration
{
    public class RelatedProductsConfiguration : EntityTypeConfiguration<RelatedProduct>
    {
        public RelatedProductsConfiguration()
        {
            this.HasMany(e => e.Products)
              .WithRequired(e => e.RelatedProduct)
              .HasForeignKey(e => e.Id)
              .WillCascadeOnDelete(true);


        }
    }
}