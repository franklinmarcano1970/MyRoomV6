using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyRoom.API.Model;
using MyRoom.Data.SampleData;
using MyRoom.Model.Entities;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyRoom.Data
{
    //public class AuthContext : IdentityDbContext<UserIdentity>
    //{
    //    public AuthContext()
    //        : base("MyRoom")
    //    {

    //    }

    //    public DbSet<Client> Clients { get; set; }
    //    public DbSet<RefreshToken> RefreshTokens { get; set; }
    //}

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    

    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        static AuthDbContext()
        {
            Database.SetInitializer(new MyRoomDatabaseInitializer());
        }

        public AuthDbContext()
            : base("MyRoom", throwIfV1Schema: false)
        {
        }

        public static AuthDbContext Create()
        {
            return new AuthDbContext();
        }
    }
}