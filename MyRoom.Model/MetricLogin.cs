using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MyRoom.Model
{
    [Table("METRICS_LOGIN")]
    [JsonObject(IsReference = true)]
    public partial class MetricLogin
    {
        public MetricLogin()
        {
        }

        [Key]
        [Column("Id")]
        public int MetricLoginId { get; set; }

        [Column("IdHotel")]
        public int HotelId { get; set; }

        [Column("IdGuest")]
        public int GuestId { get; set; }


        [StringLength(150)]
        public string Action{ get; set; }

        [Column(TypeName = "date")]
        public DateTime? ActionDateTime { get; set; }

    }
}
