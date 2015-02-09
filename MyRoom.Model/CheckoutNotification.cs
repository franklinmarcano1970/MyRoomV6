using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MyRoom.Model
{
    [Table("CHECKOUT_NOTIFICATION")]
    [JsonObject(IsReference = true)]
    public partial class CheckoutNotification
    {
        public CheckoutNotification()
        {
        }

        [Key]
        [Column("Id")]
        public int CheckoutNotificationId { get; set; }
        
        [Required]
        [Column("IdHotel")]
        public int HotelId { get; set; }

        [Required]
        [Column("IdRoom")]
        public int RoomId { get; set; }

        [DataType("date")]
        public DateTime? CheckoutDateTime { get; set; }

        public string Comments { get; set; }
        
        public bool? Old { get; set; }

        [DataType("date")]
        public DateTime? Date { get; set; }
    }
}
