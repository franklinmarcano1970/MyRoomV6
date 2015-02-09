using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        [Column("IdHotel")]
        public int HotelId { get; set; }
 
        [Required]
        public int Number { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Column("IdRoomType")]
        public int RoomTypeId { get; set; }

        [Required]
        public bool IsEmpty { get; set; }

        [Required]
        public bool IsReadForUse { get; set; }


        [Required]
        public bool Active { get; set; }

        [Required]
        public bool Standard { get; set; }

        [Required]
        public bool Premium { get; set; }

    }
}
