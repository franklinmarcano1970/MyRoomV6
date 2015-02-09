using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public int GuestId { get; set; }


        [Column("IdHotel")]
        public int HotelId { get; set; }

        [Column("IdRoom")]
        public int RoomId { get; set; }


        [Column(TypeName = "date")]
        public DateTime? CheckinDateTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CheckoutDateTime { get; set; }
    
    }
}