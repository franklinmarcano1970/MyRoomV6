using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Configuration
{
    public class MenuAccessConfiguration : EntityTypeConfiguration<MenuAccess>
    {
        public MenuAccessConfiguration()
        {
            this.HasMany(e => e.Permissions)
            .WithRequired(e => e.MenuAccess)
            .HasForeignKey(e => e.IdPermission)
            .WillCascadeOnDelete(false);
        }
    }
}