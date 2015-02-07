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
            HasMany(e => e.UserHotelPermissions)
            .WithRequired(e => e.Hotel)
            .HasForeignKey(e => e.IdHotel)
            .WillCascadeOnDelete(true);

            HasMany(e => e.HotelCatalogues)
            .WithRequired(e => e.Hotel)
            .HasForeignKey(e => e.IdHotel)
            .WillCascadeOnDelete(true);

            HasMany(e => e.ActiveHotelProducts)
            .WithRequired(e => e.Hotel)
            .HasForeignKey(e => e.IdHotel)
            .WillCascadeOnDelete(true);

            HasMany(e => e.ActiveHotelCategories)
           .WithRequired(e => e.Hotel)
           .HasForeignKey(e => e.IdHotel)
           .WillCascadeOnDelete(true);

            HasMany(e => e.ActiveHotelModules)
           .WithRequired(e => e.Hotel)
           .HasForeignKey(e => e.IdHotel)
           .WillCascadeOnDelete(true);
        }
    }
}