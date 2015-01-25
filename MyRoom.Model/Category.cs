using MyRoom;
namespace MyRoom.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [JsonObject(IsReference = false)]
    [Table("CATEGORIES")]
    public partial class Category
    {
        public Category()
        {
            ActiveHotelCategory = new HashSet<ActiveHotelCategory>();
            //RelCategoryProduct = new HashSet<RelCategoryProduct>();
            //RelModuleCategory = new HashSet<RelModuleCategory>();
            RelUserCategory = new HashSet<RelUserCategory>();
            //CategoryChild = new Category();
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("Id")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int IdTranslationName { get; set; }

        public string Image { get; set; }

        public int? IdParentCategory { get; set; }

        public int? CategoryItem { get; set; }

        public bool IsFirst { get; set; }

        public bool IsFinal { get; set; }

        public bool Active { get; set; }

        public string Comment { get; set; }

        public int? Orden { get; set; }

        public bool? Pending { get; set; }

        public string Prefix { get; set; }

        [JsonIgnore]
        public ICollection<Module> Modules { get; set; }

     
        public virtual ICollection<Product> Products { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<ActiveHotelCategory> ActiveHotelCategory { get; set; }

        public virtual Translation Translation { get; set; }

       
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }

        //public virtual ICollection<RelModuleCategory> RelModuleCategory { get; set; }
        [JsonIgnore]
        public virtual ICollection<RelUserCategory> RelUserCategory { get; set; }

        [NotMapped]
        public Category CategoryChildren { get; set; }
    }
}
