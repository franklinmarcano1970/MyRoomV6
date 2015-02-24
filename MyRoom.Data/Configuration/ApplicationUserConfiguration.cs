using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Configuration
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
           HasMany(e => e.Permissions)
           .WithRequired(e => e.User)
           .HasForeignKey(e => e.IdUser)
           .WillCascadeOnDelete(true);

           HasMany(e => e.UserHotelPermissions)
              .WithRequired(e => e.User)
              .HasForeignKey(e => e.IdUser)
              .WillCascadeOnDelete(true);

           HasMany(e => e.RelUserCatalogue)
            .WithRequired(e => e.User)
            .HasForeignKey(e => e.IdUser)
            .WillCascadeOnDelete(false);

           HasMany(e => e.RelUserCategory)
          .WithRequired(e => e.User)
          .HasForeignKey(e => e.IdUser)
          .WillCascadeOnDelete(false);

           HasMany(e => e.RelUserModule)
            .WithRequired(e => e.User)
            .HasForeignKey(e => e.IdUser)
            .WillCascadeOnDelete(false);

           HasMany(e => e.RelUserProduct)
            .WithRequired(e => e.User)
            .HasForeignKey(e => e.IdUser)
            .WillCascadeOnDelete(false);

        }
    }
}