namespace MyRoom.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("REL_USER_CATALOGUE")]
    public partial class RelUserCatalogue
    {
        public int Id { get; set; }

        public string IdUser { get; set; }

        public int IdCatalogue { get; set; }

        public bool? ReadOnly { get; set; }

        public bool? ReadWrite { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Catalog Catalog { get; set; }
    }
}
