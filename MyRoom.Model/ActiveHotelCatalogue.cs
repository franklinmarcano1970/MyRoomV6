namespace MyRoom.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ACTIVE_HOTEL_CATALOGUE")]
    public partial class ActiveHotelCatalogue
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdHotel { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCatalogue { get; set; }

        public bool Active { get; set; }

        public virtual Hotel Hotel { get; set; }

        public virtual Catalog Catalog { get; set; }
    }
}
