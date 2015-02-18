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
    [Table("GUESTS")]
    [JsonObject(IsReference = true)]
    public class Guest
    {
        [Key]
        [Column("Id")]
        public int GuestId { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar")]       
        [StringLength(150)]
        public string Surname { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(25)]
        public string Password { get; set; }

        [Required]

        [Column(TypeName = "varchar")]
        [StringLength(25)]
        public string Dni { get; set; }

        [Required]
        [StringLength(20)]
        [Column("Sexo", TypeName="varchar")]
        public string Gender { get; set; }

        [DefaultValue(0)]        
        public int? Edad { get; set; }

        [Column("IdHotel")]
        public int? HotelId { get; set; }

        [Column("IdRoom")]
        public int? RoomId { get; set; }


        [Column(TypeName = "smalldatetime")]
        public DateTime? CheckinDateTime { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CheckoutDateTime { get; set; }
    
        [StringLength(150)]
        [Column("Email2", TypeName="varchar")]
        public string OtherEmail { get; set; }

        [Required]
        [DefaultValue("true")]        
        public bool Active { get; set; }

    }
}