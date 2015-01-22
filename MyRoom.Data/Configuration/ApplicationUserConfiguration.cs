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
           this.HasMany(e => e.Permissions)
           .WithRequired(e => e.User)
           .HasForeignKey(e => e.IdUser)
           .WillCascadeOnDelete(true);

           this.HasMany(e => e.UserHotelPermissions)
              .WithRequired(e => e.User)
              .HasForeignKey(e => e.IdUser)
              .WillCascadeOnDelete(true);
        }
    }
}