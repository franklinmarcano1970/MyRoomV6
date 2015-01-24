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

        this.HasMany(c => c.Products)
    .WithRequired()
    .HasForeignKey(c => c.Id);

                               this.HasKey(c => new { c.IdProduct, c.IdRelatedProduct});

//            modelBuilder.Entity<RELATED_PRODUCTS>()
//.HasMany(e => e.PRODUCTS)
//.WithOptional(e => e.RELATED_PRODUCTS)
//.HasForeignKey(e => new { e.RelatedProduct_IdProduct, e.RelatedProduct_IdRelatedProduct });


            //this.HasKey(c => new { c.IdProduct, c.IdRelatedProduct });
            
            //this.HasMany(e => e.RelatedProducts)
            // .WithRequired(e => e.Product)
            // .HasForeignKey(e => e.IdRelatedProduct)
            // .WillCascadeOnDelete(true);


            //this.HasKey(c => new { c.IdProduct, c.IdRelatedProduct });

            //this.HasMany(e => e.Products)
            //  .WithRequired(e => e.)
            //  .HasForeignKey(e => e.Id)
            //  .WillCascadeOnDelete(true);


        }
    }
}