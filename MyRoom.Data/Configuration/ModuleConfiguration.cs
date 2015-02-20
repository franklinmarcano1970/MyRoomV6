using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Configuration
{
    public class ModuleConfiguration : EntityTypeConfiguration<Module>
    {
        public ModuleConfiguration()
        {
            HasKey(x => x.ModuleId);
            //Property(x => x.TerritoryDescription).HasColumnType("nchar").HasMaxLength(50).IsRequired();

            HasMany(x => x.Catalogues)
            .WithMany(x => x.Modules)
            .Map(mc =>
            {
                mc.MapLeftKey("IdModule");
                mc.MapRightKey("IdCatalogue");
                mc.ToTable("REL_CATALOGUE_MODULE");
            });

            HasMany(e => e.RelUserModule)
            .WithRequired(e => e.Module)
            .HasForeignKey(e => e.IdModule)
            .WillCascadeOnDelete(false);
        }
    }
}