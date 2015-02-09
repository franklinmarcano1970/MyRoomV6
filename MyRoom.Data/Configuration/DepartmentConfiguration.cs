using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Configuration
{
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            //HasMany(e => e.Hotels)
            //        .WithRequired(e => e.Department)
            //        .HasForeignKey(e => e.HotelId)
            //        .WillCascadeOnDelete(false);

        }
    }
}