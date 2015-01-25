namespace MyRoom.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
     
    [JsonObject(IsReference = false)] 
    [Table("MODULES")]
    public partial class Module
    {

        public Module()
        {
            ActiveHotelModule = new HashSet<ActiveHotelModule>();
         //   RelCatalogueModule = new HashSet<RelCatalogueModule>();
            //RelModuleCategory = new HashSet<RelModuleCategory>();
            RelUserModule = new HashSet<RelUserModule>();
        }

        [Key]
        [Column("Id")]
        public int ModuleId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int IdTranslationName { get; set; }

        public string Image { get; set; }

        public bool Active { get; set; }

        public string Comment { get; set; }

        public bool? Pending { get; set; }

        public int? Orden { get; set; }

        public string Prefix { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Catalog> Catalogues { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        [JsonIgnore]
        public virtual ICollection<ActiveHotelModule> ActiveHotelModule { get; set; }

        public virtual Translation Translation { get; set; }

       // public virtual ICollection<RelCatalogueModule> RelCatalogueModule { get; set; }

        //public virtual ICollection<RelModuleCategory> RelModuleCategory { get; set; }
       [JsonIgnore]
        public virtual ICollection<RelUserModule> RelUserModule { get; set; }
    }
}
