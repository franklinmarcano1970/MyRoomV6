using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MyRoom.Model
{
    [Table("ORDERS")]
    [JsonObject(IsReference = true)]
    public partial class Order
    {
        public Order()
        {
        }

        [Key]
        [Column("Id")]
        public int OrderId { get; set; }

        [Required]
        [DefaultValue(0)]
        [Column("IdHotel")]
        public int HotelId { get; set; }

        [Required]
        [DefaultValue(0)]
        [Column("IdGuest")]
        public int GuestId { get; set; }

        [Required]
        public string Reference { get; set; }

        public int IdTranslationReference { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? OrderDateTime { get; set; }

        [StringLength(200)]
        public string Place { get; set; }

        [Column(TypeName="text")]
        public string Comment { get; set; }

        [DefaultValue(0)]
        public int Status { get; set; }

        [DefaultValue(0)]
        public bool Old { get; set; }

        public virtual Translation Translation { get; set; }

    }
}
