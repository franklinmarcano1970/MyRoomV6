using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MyRoom.Model
{
    [Table("CHECKOUT_NOTIFICATIONS")]
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
        [DefaultValue(0)]
        [Column("IdHotel")]
        public int HotelId { get; set; }

        [Required]
        [DefaultValue(0)]
        [Column("IdRoom")]
        public int RoomId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CheckoutDateTime { get; set; }

        public string Comments { get; set; }

        [DefaultValue("false")]
        public bool? Old { get; set; }

        [Column(TypeName="smalldatetime")]
        public DateTime? Date { get; set; }
    }
}
