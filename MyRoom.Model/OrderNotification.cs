using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MyRoom.Model
{
    [Table("ORDER_NOTIFICATION")]
    [JsonObject(IsReference = true)]
    public partial class OrderNotification
    {
        public OrderNotification()
        {
        }

        [Key]
        [Column("Id")]
        public int OrderNotificationId { get; set; }

        [Required]
        [Column("IdRoomDestination")]
        public int RoomDestinationId { get; set; }

        [Required]
        [Column("IdHotel")]
        public int HotelId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NotificationDateTime { get; set; }

        public string Reply { get; set; }

        public bool Old { get; set; }

        public string Comments { get; set; }

    }
}
