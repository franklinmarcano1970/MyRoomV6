namespace MyRoom.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CATALOGUES")]
    public partial class Catalog
    {
        public Catalog()
        {
            ActiveHotelCatalogue = new HashSet<ActiveHotelCatalogue>();
    //        RelCatalogueModule = new HashSet<RelCatalogueModule>();
            RelUserCatalogue = new HashSet<RelUserCatalogue>();
        }

        [Key]
        [Column("Id")]
        public int CatalogId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int IdTranslationName { get; set; }

        public string Image { get; set; }

        public bool Active { get; set; }

        public string Comment { get; set; }

        public bool? Pending { get; set; }


        public ICollection<Module> Modules { get; set; }


        public  ICollection<ActiveHotelCatalogue> ActiveHotelCatalogue { get; set; }

        public Translation Translation { get; set; }

        //public  ICollection<RelCatalogueModule> RelCatalogueModule { get; set; }

        public  ICollection<RelUserCatalogue> RelUserCatalogue { get; set; }
    }
}
