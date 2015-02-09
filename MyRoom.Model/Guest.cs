﻿using Microsoft.AspNet.Identity;
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
    [Table("GUESTS")]
    [JsonObject(IsReference = true)]
    public class Guest
    {
        [Key]
        [Column("Id")]
        public int GuestId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Surname { get; set; }
              
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(25)]
        public string Dni { get; set; }

        [Required]
        [StringLength(20)]
        [Column("Sexo")]
        public string Gender { get; set; }
    
        public int? Edad { get; set; }

        [Column("IdHotel")]
        public int? HotelId { get; set; }

        [Column("IdRoom")]
        public int? RoomId { get; set; }


        [Column(TypeName = "date")]
        public DateTime? CheckinDateTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CheckoutDateTime { get; set; }
    
        [StringLength(150)]
        [Column("Email2")]
        public string OtherEmail { get; set; }

        [Required]
        public bool Active { get; set; }

    }
}