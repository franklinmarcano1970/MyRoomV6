namespace MyRoom.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [JsonObject(IsReference = false)]
    [Table("CATALOGUES")]
    public partial class Catalog
    {
        public Catalog()
        {
            HotelCatalogues = new HashSet<ActiveHotelCatalogue>();
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


        public virtual ICollection<Module> Modules { get; set; }


       // public  ICollection<ActiveHotelCatalogue> ActiveHotelCatalogue { get; set; }

        public virtual Translation Translation { get; set; }

        //public  ICollection<RelCatalogueModule> RelCatalogueModule { get; set; }
        public virtual ICollection<ActiveHotelCatalogue> HotelCatalogues { get; set; }

        public  ICollection<RelUserCatalogue> RelUserCatalogue { get; set; }
    }
}
