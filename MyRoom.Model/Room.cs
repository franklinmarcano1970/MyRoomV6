using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MyRoom.Model
{
    [Table("ROOMS")]
    [JsonObject(IsReference = true)]
    public partial class Room
    {
        public Room()
        {
        }

        [Key]
        [Column("Id")]
        public int RoomId { get; set; }
        
        [Required]
        [DefaultValue(0)]
        [Column("IdHotel")]
        public int HotelId { get; set; }
 
        [Required]
        [DefaultValue(0)]
        public int Number { get; set; }

        [Required]
        [Column(TypeName="varchar")]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [DefaultValue(1)]
        public bool IsEmpty { get; set; }

        [Required]
        [DefaultValue(1)]
        public bool IsReadForUse { get; set; }

        [Required]
        [DefaultValue(1)]
        public bool Active { get; set; }

        [Required]
        [DefaultValue(1)]
        public bool Standard { get; set; }

        [Required]
        [DefaultValue(0)]
        public bool Premium { get; set; }

    }
}
