
using Microsoft.AspNet.Identity.EntityFramework;
using MyRoom.Data.Configuration;
using MyRoom.Data.SampleData;
using MyRoom.Model;
using System.Data.Entity;

namespace MyRoom.Data
{

    //Contexto que configura la sesion de los datos con sus normalizaciones y reglas que son heredadas de las Entidad Relacion de la base de datos.
    public partial class MyRoomDbContext : IdentityDbContext<ApplicationUser>
    {
        static MyRoomDbContext()
        {
            Database.SetInitializer(new MyRoomDatabaseInitializer());
        }

        public MyRoomDbContext()
            : base(nameOrConnectionString: "MyRoom", throwIfV1Schema: false) { }

        public static MyRoomDbContext Create()
        {
            return new MyRoomDbContext();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<ActiveHotelCatalogue> HotelCatalogues { get; set; }
        public virtual DbSet<ActiveHotelCategory> ActiveHotelCategory { get; set; }
        public virtual DbSet<ActiveHotelModule> ActiveHotelModule { get; set; }
        public virtual DbSet<ActiveHotelProduct> ActiveHotelProduct { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<UserHotelPermission> UserHotelPermissions { get; set; }
        public virtual DbSet<Catalog> Catalogues { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<MenuAccess> MenuAccess { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        //   public virtual DbSet<RelCatalogueModule> RelCatalogueModule { get; set; }
        public virtual DbSet<CategoryProduct> CategoryProducts { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderNotification> OrderNotifications { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<MetricLogin> MetricLogins { get; set; }

        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<GuestHistory> GuestHistories { get; set; }
        public virtual DbSet<Department> Departments { get; set; }


        public virtual DbSet<CheckoutNotification> CheckoutNotifications { get; set; }        
         
        //public virtual DbSet<RelModuleCategory> RelModuleCategory { get; set; }
        //public virtual DbSet<RelUserAccess> RelUserAccess { get; set; }
        public virtual DbSet<RelUserCatalogue> RelUserCatalogue { get; set; }
        public virtual DbSet<RelUserCategory> RelUserCategory { get; set; }
        //public virtual DbSet<RelUserHotel> RelUserHotel { get; set; }
        public virtual DbSet<RelUserModule> RelUserModule { get; set; }
        public virtual DbSet<RelUserProduct> RelUserProduct { get; set; }
        public virtual DbSet<RelatedProduct> RelatedProducts { get; set; }
        public virtual DbSet<Translation> Translations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CatalogConfiguration());

            modelBuilder.Configurations.Add(new ModuleConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new MenuAccessConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new ProductsConfiguration());
            modelBuilder.Configurations.Add(new HotelConfiguration());
            modelBuilder.Configurations.Add(new TranslationConfiguration());
            modelBuilder.Configurations.Add(new DepartmentConfiguration());



            modelBuilder.Configurations.Add(new RelatedProductsConfiguration());


            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id).Property(p => p.Name).IsRequired();
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Entity<IdentityUserLogin>().HasKey(u => new { u.UserId, u.LoginProvider, u.ProviderKey });

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.RelUserCatalogue)
            //    .WithRequired(e => e.User)
            //    .HasForeignKey(e => e.IdUser)
            //    .WillCascadeOnDelete(false);


            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.RelUserHotel)
            //    .WithRequired(e => e.User)
            //    .HasForeignKey(e => e.IdUser)
            //    .WillCascadeOnDelete(false);


            //modelBuilder.Entity<Catalog>()
            //    .Property(e => e.Name)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Catalog>()
            //    .Property(e => e.Image)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Catalog>()
            //    .Property(e => e.Comment)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Catalog>()
            //    .HasMany(e => e.ActiveHotelCatalogue)
            //    .WithRequired(e => e.Catalog)
            //    .HasForeignKey(e => e.IdCatalogue)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Catalog>()
            //    .HasMany(e => e.RelCatalogueModule)
            //    .WithRequired(e => e.Catalog)
            //    .HasForeignKey(e => e.IdCatalogue)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Catalog>()
            //    .HasMany(e => e.RelUserCatalogue)
            //    .WithRequired(e => e.Catalog)
            //    .HasForeignKey(e => e.IdCatalogue)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.ActiveHotelCategory)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.IdCategory)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Category>()
            //    .HasMany(e => e.RelCategoryProduct)
            //    .WithRequired(e => e.Category)
            //    .HasForeignKey(e => e.IdCategory)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Categories>()
            //    .HasMany(e => e.RelModuleCategory)
            //    .WithRequired(e => e.Categories)
            //    .HasForeignKey(e => e.IdCategory)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Category>()
            //    .HasMany(e => e.RelUserCategory)
            //    .WithRequired(e => e.Categories)
            //    .HasForeignKey(e => e.IdCategory)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Hotel>()
            //    .Property(e => e.Name)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Hotel>()
            //    .Property(e => e.Image)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Hotel>()
            //    .HasMany(e => e.ActiveHotelCatalogue)
            //    .WithRequired(e => e.Hotel)
            //    .HasForeignKey(e => e.IdHotel)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Hotel>()
            //    .HasMany(e => e.ActiveHotelCategory)
            //    .WithRequired(e => e.Hotel)
            //    .HasForeignKey(e => e.IdHotel)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Hotel>()
            //    .HasMany(e => e.ActiveHotelModule)
            //    .WithRequired(e => e.Hotel)
            //    .HasForeignKey(e => e.IdHotel)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Hotel>()
            //    .HasMany(e => e.ActiveHotelProduct)
            //    .WithRequired(e => e.Hotel)
            //    .HasForeignKey(e => e.IdHotel)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Hotel>()
            //    .HasMany(e => e.RelUserHotel)
            //    .WithRequired(e => e.Hotel)
            //    .HasForeignKey(e => e.IdHotel)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<MenuAccess>()
                .Property(e => e.MainMenuOption)
                .IsUnicode(false);

            //modelBuilder.Entity<MenuAccess>()
            //    .HasMany(e => e.RelUserAccess)
            //    .WithRequired(e => e.MenuAccess)
            //    .HasForeignKey(e => e.IdPermission)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Module>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .HasMany(e => e.ActiveHotelModule)
                .WithRequired(e => e.Module)
                .HasForeignKey(e => e.IdModule)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Module>()
            //    .HasMany(e => e.RelCatalogueModule)
            //    .WithRequired(e => e.Module)
            //    .HasForeignKey(e => e.IdModule)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Module>()
            //    .HasMany(e => e.RelModuleCategory)
            //    .WithRequired(e => e.Module)
            //    .HasForeignKey(e => e.IdModule)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Module>()
                .HasMany(e => e.RelUserModule)
                .WithRequired(e => e.Module)
                .HasForeignKey(e => e.IdModule)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name_ENG)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description_ENG)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.UrlScanDocument)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ActiveHotelProduct)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Product>()
            //    .HasMany(e => e.RelCategoryProduct)
            //    .WithRequired(e => e.Product)
            //    .HasForeignKey(e => e.IdProduct)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.RelUserProduct)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Product>()
            //    .HasMany(e => e.RelatedProducts)
            //    .WithRequired(e => e.Product)
            //    .HasForeignKey(e => e.IdProduct)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Translation>()
            //    .Property(e => e.Spanish)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Translation>()
            //    .Property(e => e.English)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Translation>()
            //    .Property(e => e.French)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Translation>()
            //    .Property(e => e.German)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Translation>()
            //    .Property(e => e.Language5)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Translation>()
            //    .Property(e => e.Language6)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Translation>()
            //    .Property(e => e.Language7)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Translation>()
            //    .Property(e => e.Language8)
            //    .IsUnicode(false);

      
            //modelBuilder.Entity<Translation>()
            //    .HasMany(e => e.Products1)
            //    .WithOptional(e => e.Translation1)
            //    .HasForeignKey(e => e.IdTranslationDescription);


        }
    }
}
