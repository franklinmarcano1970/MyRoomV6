using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MyRoom.Model
{
    [Table("HOTELS")]
    [JsonObject(IsReference = true)]
    public partial class Hotel
    {
        public Hotel()
        {
            HotelCatalogues = new HashSet<ActiveHotelCatalogue>();
            //ActiveHotelCategory = new HashSet<ActiveHotelCategory>();
            //ActiveHotelModule = new HashSet<ActiveHotelModule>();
            //ActiveHotelProduct = new HashSet<ActiveHotelProduct>();
         //   Users = new HashSet<ApplicationUser>();
        }       

        [Key]
        [Column("Id")]
        public int HotelId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int IdTranslationName { get; set; }

        public string Image { get; set; }

        public bool Active { get; set; }

        public string UrlScanMap { get; set; }

        [StringLength(3)]
        public string UTC { get; set; }

        [Required]
        public bool ChangeSummerTime { get; set; }

        [Column("ContenidoIframeSurvey")]
        public string ContentIframeSurvey { get; set; }

        public  virtual ICollection<ActiveHotelCatalogue> HotelCatalogues { get; set; }

        public virtual ICollection<ActiveHotelCategory> ActiveHotelCategories { get; set; }

        public virtual ICollection<ActiveHotelModule> ActiveHotelModules { get; set; }

        public virtual ICollection<ActiveHotelProduct> ActiveHotelProducts { get; set; }

        public virtual ICollection<UserHotelPermission> UserHotelPermissions { get; set; }

      
        public virtual Translation Translation { get; set; }
        
        //public virtual Department Department { get; set; }

        //public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
