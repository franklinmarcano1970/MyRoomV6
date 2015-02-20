namespace MyRoom.Model
{
    using Newtonsoft.Json;
    using System.ComponentModel;
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

        [Required]
        [DefaultValue(true)]
        public bool Active { get; set; }
        
        [JsonIgnore]
        public virtual Hotel Hotel { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
    }
}
