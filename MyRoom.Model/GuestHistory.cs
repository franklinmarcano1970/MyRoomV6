using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyRoom.Model 
{
    [Table("GUEST_HISTORY")]
    [JsonObject(IsReference = true)]
    public class GuestHistory
    {
        [Key]
        [Column("Id")]
        public int GuestHistoryId { get; set; }

        [Column("IdGuest")]
        [DefaultValue(1)]
        public int GuestId { get; set; }


        [Column("IdHotel")]
        [DefaultValue(1)]
        public int HotelId { get; set; }

        [Column("IdRoom")]
        [DefaultValue(1)]
        public int RoomId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CheckinDateTime { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CheckoutDateTime { get; set; }
    
    }
}