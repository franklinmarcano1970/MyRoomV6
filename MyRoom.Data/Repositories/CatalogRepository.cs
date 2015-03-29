using MyRoom.Helpers;
using MyRoom.Model;
using MyRoom.Model.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MyRoom.Data.Repositories
{
    public class CatalogRepository : GenericRepository<Catalog>
    {
        public CatalogRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }

        public Catalog GetStructureComplete(int id)
        {
            var catalogues = from c in this.Context.Catalogues
                                 .Include("Translation")
                                 .Include("Modules")
                                 .Include("Modules.Translation")
                                 .Include("Modules.ActiveHotelModule")
                                 .Include("Modules.Categories")
                                 .Include("Modules.Categories.Translation")
                                 .Include("Modules.Categories.CategoryProducts")
                                 .Include("Modules.Categories.ActiveHotelCategory")
                                 .Include("Modules.RelUserModule")
                             where c.CatalogId == id && c.Active == true
                             select c;
            return catalogues.FirstOrDefault();
         
        }

        public override async Task EditAsync(Catalog entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Context.Entry(entity.Translation).State = EntityState.Modified;

            await this.Context.SaveChangesAsync();
        }

        public MyRoomDbContext Context { get; private set; }



        public string GetCatalogueById(int key)
        {
            var catalogues = from c in this.Context.Catalogues
                               .Include("Translation")

                             where c.CatalogId == key
                             select c;
            return JsonConvert.SerializeObject(catalogues.First(), Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
        }
    }
}