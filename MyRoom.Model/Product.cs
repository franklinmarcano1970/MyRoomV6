using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MyRoom.Model
{
    [JsonObject(IsReference = false)]
    [Table("PRODUCTS")]
    public class Product
    {

        public Product()
        {
            ActiveHotelProduct = new List<ActiveHotelProduct>();
            CategoryProducts = new HashSet<CategoryProduct>();
            RelUserProduct = new HashSet<RelUserProduct>();
            // RelatedProducts = new HashSet<RelatedProduct>();
        }

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [DefaultValue(1)]
        public int IdTranslationName { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [DefaultValue(1)]
        public int IdTranslationDescription { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [StringLength(50)]
        [DefaultValue("product")]
        public string Type { get; set; }

        [Required]
        public bool Active { get; set; }


        [StringLength(10)]
        public string Prefix { get; set; }


        public int? IdCategoryCrossSelling { get; set; }

        [StringLength(150)]
        public string Name_ENG { get; set; }

        [Column(TypeName = "text")]
        public string Description_ENG { get; set; }

        public string UrlScanDocument { get; set; }

        public bool? Pending { get; set; }

        [Required]
        public bool Standard { get; set; }

        [Required]
        public bool Premium { get; set; }

        [StringLength(150)]
        public string EmailMoreInfo { get; set; }


        [Column("Orden")]
        public int? Order { get; set; }
        [JsonIgnore]
        public virtual List<ActiveHotelProduct> ActiveHotelProduct { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<Category> Categories { get; set; }
        public virtual Translation Translation { get; set; }

        public virtual Translation TranslationDescription { get; set; }


        //   public virtual Translation Translation1 { get; set; }
        [JsonIgnore]
        public ICollection<CategoryProduct> CategoryProducts { get; set; }
        //[JsonIgnore]
        public virtual ICollection<RelUserProduct> RelUserProduct { get; set; }

        public virtual ICollection<RelatedProduct> RelatedProducts { get; set; }


    }
}
