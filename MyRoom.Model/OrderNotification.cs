using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DefaultValue(0)]
        [Column("IdRoomDestination")]
        public int RoomDestinationId { get; set; }
        
        public string Reply { get; set; }


        [Required]
        [DefaultValue(0)]
        [Column("IdHotel")]
        public int HotelId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NotificationDateTime { get; set; }

        [Required]
        [DefaultValue(0)]        
        public bool Old { get; set; }

        [Column(TypeName="text")]
        public string Comments { get; set; }

    }
}
