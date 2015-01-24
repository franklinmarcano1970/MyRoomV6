namespace MyRoom.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RELATED_PRODUCTS")]
    public partial class RelatedProduct
    {
        public RelatedProduct()
        {
            Products = new HashSet<Product>();
        }
        //[Column(Order = 1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdProduct { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdRelatedProduct { get; set; }

        public Product Product { get; set; }


        //public ICollection<Product> Products { get; set; }

        //public ICollection<Product> Products { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
