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
    [JsonObject(IsReference = true)]
    [Table("DEPARTMENTS")]
    public class Department
    {

        public Department()
        {
         //   Hotels = new HashSet<Hotel>();
        }

        [Key]
        [Column("Id")]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int IdTranslationName { get; set; }

        [StringLength(150)]
        public string Director { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [DefaultValue("true")]
        public bool Active { get; set; }

        [Required]
        [DefaultValue(0)]
        [Column("IdHotel")]
        public int? HotelId { get; set; }

        [Required]
        [DefaultValue(0)]
        public bool IsExternal { get; set; }

        public virtual Translation Translation { get; set; }

        //[JsonIgnore]
        //public ICollection<Hotel> Hotels { get; set; }
    }
}