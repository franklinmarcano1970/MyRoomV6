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
            //this.HasMany(e => e.RelatedProducts)
            //  .WithRequired(e => e.Product)
            //  .HasForeignKey(e => e.IdProduct)
            //  .WillCascadeOnDelete(true);


  
                this.HasMany(x => x.RelatedProducts)
                .WithMany(x => x.Products)
            .Map(x =>
            {
                x.ToTable("RELATED_PRODUCTS"); // third table is named Cookbooks
                x.MapLeftKey("IdProduct");
                x.MapRightKey("IdRelatedProduct");
            });
    

            HasMany(e => e.CategoryProducts)
            .WithRequired(e => e.Product)
            .HasForeignKey(e => e.IdProduct)
            .WillCascadeOnDelete(true);
        }
    }
}