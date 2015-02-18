using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MyRoom.Model
{
    [Table("ORDER_DETAILS")]
    [JsonObject(IsReference = true)]
    public partial class OrderDetail
    {
        public OrderDetail()
        {
        }

        [Key]
        [Column("Id")]
        public int OrderDetailId { get; set; }

        [Required]
        [Column("IdOrder")]
        public int OrderId { get; set; }

        [Required]
        [Column("IdProduct")]
        public int ProductId { get; set; }

        [DefaultValue(0)]        
        public int? Quantity { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ServiceDateTime { get; set; }

        [Required]
        [DefaultValue(0)]        
        public int Status { get; set; }
    }
}
