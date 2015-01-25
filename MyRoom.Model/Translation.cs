namespace MyRoom.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TRANSLATIONS")]
    public partial class Translation
    {
        public Translation()
        {
            Catalogues = new HashSet<Catalog>();
            Categories = new HashSet<Category>();
            Hotels = new HashSet<Hotel>();
            Modules = new HashSet<Module>();
            Products = new HashSet<Product>();
        //    Products1 = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Spanish { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string English { get; set; }

        [Column(TypeName = "text")]
        public string French { get; set; }

        [Column(TypeName = "text")]
        public string German { get; set; }

        public bool Active { get; set; }

        [Column(TypeName = "text")]
        public string Language5 { get; set; }

        [Column(TypeName = "text")]
        public string Language6 { get; set; }

        [Column(TypeName = "text")]
        public string Language7 { get; set; }

        [Column(TypeName = "text")]
        public string Language8 { get; set; }

        [JsonIgnore]
        public ICollection<Catalog> Catalogues { get; set; }

        [JsonIgnore]
        public ICollection<Category> Categories { get; set; }

        [JsonIgnore]
        public ICollection<Hotel> Hotels { get; set; }           
       
        public ICollection<Module> Modules { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
        
        //[JsonIgnore]
        //public virtual ICollection<Product> Products1 { get; set; }
    }
}
