using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Configuration
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            HasKey(x => x.CategoryId);
            //Property(x => x.TerritoryDescription).HasColumnType("nchar").HasMaxLength(50).IsRequired();

            HasMany(x => x.Modules)
            .WithMany(x => x.Categories)
            .Map(mc =>
            {
                mc.MapLeftKey("IdCategory");
                mc.MapRightKey("IdModule");
                mc.ToTable("REL_MODULE_CATEGORY");
            });


            HasMany(e => e.CategoryProducts)
               .WithRequired(e => e.Category)
               .HasForeignKey(e => e.IdCategory)
               .WillCascadeOnDelete(true);

            //HasKey(x => x.CategoryId);
            //HasMany(x => x.Products)
            //.WithMany(x => x.Categories)
            //.Map(mc =>
            //{
            //    mc.MapLeftKey("IdCategory");
            //    mc.MapRightKey("IdProduct");
            //    mc.ToTable("REL_CATEGORY_PRODUCT");
            //});

        }
    }
}