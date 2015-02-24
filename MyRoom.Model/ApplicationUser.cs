using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyRoom.Model 
{

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Permissions = new HashSet<Permission>();
        }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationUserId
        {
            get;
            set;
        }

        [Required]
        [StringLength(100)]
        public string Name
        {
            get;
            set;
        }

        [Required]
        [StringLength(100)]
        public string Surname
        {
            get;
            set;
        }

        [Required]
        public bool Active { get; set; }


        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<UserHotelPermission> UserHotelPermissions { get; set; }
        public virtual ICollection<RelUserCatalogue> RelUserCatalogue { get; set; }
        public virtual ICollection<RelUserCategory> RelUserCategory { get; set; }
        public virtual ICollection<RelUserModule> RelUserModule { get; set; }
        public virtual ICollection<RelUserProduct> RelUserProduct { get; set; }
                                                                  


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

    }
}