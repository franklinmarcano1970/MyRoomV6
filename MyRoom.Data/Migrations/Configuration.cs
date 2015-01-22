using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyRoom.Data.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyRoom.Data.MyRoomDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MyRoom.Data.MyRoomDbContext context)
        {

            var userId = string.Empty;

            if (!context.Users.Any())
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                var testUser = new ApplicationUser { UserName = "jcvaldes@gmail.com" };
                testUser.Name = "Juan";
                testUser.Surname = "Valdes";
                testUser.Active = true;
                testUser.Email = "jcvaldes.ingenieria@gmail.com";

                manager.Create(testUser, "JCV6060");
                userId = testUser.Id;
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
