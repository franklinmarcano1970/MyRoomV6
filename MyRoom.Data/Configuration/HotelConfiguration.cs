using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Configuration
{
    public class HotelConfiguration : EntityTypeConfiguration<Hotel>
    {
        public HotelConfiguration()
        {
            this.HasMany(e => e.UserHotelPermissions)
               .WithRequired(e => e.Hotel)
               .HasForeignKey(e => e.IdHotel)
               .WillCascadeOnDelete(true);


                this.HasMany(e => e.HotelCatalogues)
                .WithRequired(e => e.Hotel)
                .HasForeignKey(e => e.IdHotel)
                .WillCascadeOnDelete(true);

        }
    }
}