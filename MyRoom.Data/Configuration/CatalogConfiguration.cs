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
         
            HasMany(e => e.HotelCatalogues)
                .WithRequired(e => e.Catalog)
                .HasForeignKey(e => e.IdCatalogue)
                .WillCascadeOnDelete(true);


            HasMany(e => e.RelUserCatalogue)
             .WithRequired(e => e.Catalog)
             .HasForeignKey(e => e.IdCatalogue)
             .WillCascadeOnDelete(false);

        }
    }
}