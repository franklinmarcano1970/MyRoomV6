using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRoom.Model
{
    [Table("ROOM_TYPES")]
    [JsonObject(IsReference = true)]
    public partial class RoomType
    {
        [Key]
        [Column("Id")]
        public int RoomTypeId { get; set; }

        [DefaultValue(0)]
        [Column("IdHotel")]
        public int HotelId { get; set; }

        [Required]
        [Column(TypeName="varchar")]
        [StringLength(150)]
        public string Name { get; set; }

        public int IdTranslationName { get; set; }

        public virtual Translation Translation { get; set; }

    }
}
