using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Configuration
{
    public class TranslationConfiguration : EntityTypeConfiguration<Translation>
    {
        public TranslationConfiguration()
        {
            this.HasMany(e => e.Hotels)
            .WithRequired(e => e.Translation)
            .HasForeignKey(e => e.IdTranslationName)
            .WillCascadeOnDelete(true);

        }
    }
}