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
            Products1 = new HashSet<Product>();
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Spanish { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string English { get; set; }

        [Column(TypeName = "ntext")]
        public string French { get; set; }

        [Column(TypeName = "ntext")]
        public string German { get; set; }

        [DefaultValue(true)]
        public bool Active { get; set; }

        [Column(TypeName = "ntext")]
        public string Language5 { get; set; }

        [Column(TypeName = "ntext")]
        public string Language6 { get; set; }

        [Column(TypeName = "ntext")]
        public string Language7 { get; set; }

        [Column(TypeName = "ntext")]
        public string Language8 { get; set; }

        [JsonIgnore]
        public ICollection<Catalog> Catalogues { get; set; }

        [JsonIgnore]
        public ICollection<Category> Categories { get; set; }

        [JsonIgnore]
        public ICollection<Hotel> Hotels { get; set; }
         
        [JsonIgnore]
        public ICollection<Module> Modules { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }

        [JsonIgnore]
        public ICollection<Order> Orders { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products1 { get; set; }

        [JsonIgnore]
        public ICollection<Department> Departments { get; set; }
                

    }
}
