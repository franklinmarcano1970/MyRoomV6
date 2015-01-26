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


            // Relationships
            this.HasOptional(t => t.Product)
                .WithMany(t => t.RelatedProducts)
                .HasForeignKey(d => d.IdRelatedProduct).WillCascadeOnDelete(true);
        }
    }
}