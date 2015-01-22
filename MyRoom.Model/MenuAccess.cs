namespace MyRoom.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MENU_ACCESS")]
    public partial class MenuAccess
    {
        
        public MenuAccess()
        {
            Permissions = new HashSet<Permission>();
        }

        [Column("Id")]
        public int MenuAccessId { get; set; }

        [Required]
        [StringLength(200)]
        public string MainMenuOption { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
