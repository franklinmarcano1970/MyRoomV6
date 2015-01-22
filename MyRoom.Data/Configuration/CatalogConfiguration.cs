using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Configuration
{
    public class CatalogConfiguration : EntityTypeConfiguration<Catalog>
    {
        public CatalogConfiguration()
        {
            this.HasKey(x => x.CatalogId)           
            .Property(e => e.Name)
            .IsUnicode(false);

            this.Property(e => e.Image)
                .IsUnicode(false);

            this.Property(e => e.Comment)
                .IsUnicode(false);
        }
    }
}