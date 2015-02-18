using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using MyRoom.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using MyRoom.Data.Repositories;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using MyRoom.Helpers;
using System.Web;

namespace MyRoom.Data.SampleData
{
    public class MyRoomDatabaseInitializer :
     // CreateDatabaseIfNotExists<MyRoomDbContext>      // when model is stable
    DropCreateDatabaseIfModelChanges<MyRoomDbContext> // when iterating
    {
        // I think we can say definitively that EF is NOT a good way to add a lot of new records.
        // Never has been really. Not built for that.
        // People should (and do) switch to ADO and bulk insert for that kind of thing
        // It's really for interactive apps with humans driving data creation, not machines
        private const int AttendeesWithFavoritesCount = 4;

        protected override void Seed(MyRoomDbContext context)
        {
            // Seed code here   

            var userId = string.Empty;
            var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var testUser = new ApplicationUser { UserName = "jcvaldes@gmail.com" };
            testUser.Name = "Juan";
            testUser.Surname = "Valdes";
            testUser.Active = true;
            testUser.Email = "jcvaldes.ingenieria@gmail.com";
         
            UserManager.Create(testUser, "JCV6060");
        
            userId = testUser.Id;

            string roleName = "Admins";
            
            IdentityResult roleResult;
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleName));
            }
            UserManager.AddToRole(userId, roleName);


            roleName = "Customers";
            if (!RoleManager.RoleExists(roleName))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleName));
            }

            this.AddMenuAccessOptions(context);
            this.AddProduct(context);
            //Build Token Tables
            this.BuildClientsList(context);
            
            this.AddHotel(context);

       }

        private void AddProduct(MyRoomDbContext context)
        {       
            Product product = new Product()
            {
                Name = "ProductTest",
                Active = true,
                Description = "DescriptionTest",
                Prefix = "PR1",
                Price=12,
                Type = "text",
                Image = "img/product1.jpg",
                Translation = new Translation
                {
                    Spanish = "Introducir Texto",
                    English = "Introducir Texto",
                    French = "Introducir Texto",
                    Active = true
                },
                TranslationDescription = new Translation
                {
                    Spanish = "Introducir Texto",
                    English = "Introducir Texto",
                    French = "Introducir Texto",
                    Active = true
                }
            };

            context.Products.Add(product);
            context.SaveChanges();
        }

        /// <summary>
        /// Es usado para el Token
        /// </summary>
        private void BuildClientsList(MyRoomDbContext context)
        {

            List<Client> ClientsList = new List<Client> 
            {
                new Client
                { Id = "ngAuthApp", 
                    Secret= Helper.GetHash("myroom@123"), 
                    Name="MyRoom Application", 
                    ApplicationType = ApplicationTypes.JavaScript, 
                    Active = true, 
                    RefreshTokenLifeTime = 7200, 
                    AllowedOrigin = HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port

                },
                new Client
                { Id = "consoleApp", 
                    Secret=Helper.GetHash("myroom@abc"), 
                    Name="Console Application", 
                    ApplicationType = ApplicationTypes.NativeConfidential, 
                    Active = true, 
                    RefreshTokenLifeTime = 14400, 
                    AllowedOrigin = "*"
                }
            };

            context.Clients.AddRange( ClientsList);
            context.SaveChanges();
        }

        private void AddMenuAccessOptions(MyRoomDbContext context)
        {
             MenuAccessRepository menuRepo = new MenuAccessRepository(context);
            List<MenuAccess> options = new List<MenuAccess> {
                new MenuAccess  {MainMenuOption = "Create/Modify Catalogue Structure"},
                new MenuAccess  {MainMenuOption = "Create/Modify Products"},
                new MenuAccess  {MainMenuOption = "Assign Products to Catalogues"},
                new MenuAccess  {MainMenuOption = "Assign Catalogues to Hotels"},
                new MenuAccess  {MainMenuOption = "Activate Products to Hotel"},
                new MenuAccess  {MainMenuOption = "Backend User Management"}
            };
            try
            {
                options.ForEach(delegate(MenuAccess op)
                {
                    menuRepo.Insert(op);
                });              

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //private object AddCategories(MyRoomDbContext context)
        //{
        //    var categories = new List<Catalogue>();
        //    categories.Add(new Catalogue()
        //    {

        //        Name = "Paseos En Barco",
        //        Active = true,
        //        Image = "paseosenbarco.jpg",
        //        Translation = new Translation
        //        {
        //            Spanish = "Paseos En Barco",
        //            English = "Paseos En Barco",
        //            French = "Paseos En Barco",
        //            Active = true
        //        },                
        //    });


        //    // Done populating Catalogues
        //    categories.ForEach(ht => context.Catalogues.Add(ht));
        //    context.SaveChanges();
        //    return categories;
        //}


        private void AddHotel(MyRoomDbContext context)
        {
            Hotel hotel = new Hotel()
            {
                Name = "HotelTest1",
                Active = true,
                Translation = new Translation
                {
                    Spanish = "HotelTest1",
                    English = "HotelTest1",
                    French = "HotelTest1",
                    Active = true
                }
            };
            context.Hotels.Add(hotel);
            context.SaveChanges();
        }

    }
}