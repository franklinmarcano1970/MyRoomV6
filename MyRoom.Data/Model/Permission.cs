namespace MyRoom.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("REL_USER_ACCESS")]
    public partial class Permission
    {

        [Key]
        [Column(TypeName = "nvarchar",  Order = 0)]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string IdUser { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdPermission { get; set; }

        public virtual ApplicationUser  User { get; set; }

        public virtual MenuAccess MenuAccess { get; set; }
   
    }
}
