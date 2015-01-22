namespace MyRoom.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ACTIVE_HOTEL_CATEGORY")]
    public partial class ActiveHotelCategory
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdHotel { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCategory { get; set; }

        public bool Active { get; set; }

        public virtual Hotel Hotel { get; set; }

        public virtual Category Category { get; set; }
    }
}
