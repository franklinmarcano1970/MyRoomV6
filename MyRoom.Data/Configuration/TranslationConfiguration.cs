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
            HasMany(e => e.Hotels)
            .WithRequired(e => e.Translation)
            .HasForeignKey(e => e.IdTranslationName)
            .WillCascadeOnDelete(false);

            HasMany(e => e.Catalogues)
            .WithRequired(e => e.Translation)
            .HasForeignKey(e => e.IdTranslationName)
            .WillCascadeOnDelete(false);

            HasMany(e => e.Categories)
            .WithRequired(e => e.Translation)
            .HasForeignKey(e => e.IdTranslationName)
            .WillCascadeOnDelete(false);

            HasMany(e => e.Hotels)
            .WithRequired(e => e.Translation)
            .HasForeignKey(e => e.IdTranslationName)
            .WillCascadeOnDelete(false);

            HasMany(e => e.Modules)
            .WithRequired(e => e.Translation)
            .HasForeignKey(e => e.IdTranslationName)
            .WillCascadeOnDelete(false);

            HasMany(e => e.Products)
            .WithRequired(e => e.Translation)
            .HasForeignKey(e => e.IdTranslationName)
            .WillCascadeOnDelete(true);

            HasMany(e => e.Departments)
            .WithRequired(e => e.Translation)
            .HasForeignKey(e => e.IdTranslationName)
            .WillCascadeOnDelete(false);

        }
    }
}