using MyRoom.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyRoom.Data.Repositories
{
    public class CatalogRepository : GenericRepository<Catalog>
    {
        public CatalogRepository(MyRoomDbContext context)
            : base(context)
        {
             this.Context = context;
        }

        // public MyRoomDbContext Context  { get; private set; }

        public Catalog GetStructureComplete(int id)
        {
            var catalogues = from c in this.Context.Catalogues
                                 .Include("Translation")
                                 .Include("Modules")
                                 .Include("Modules.Categories")
                                 .Include("Modules.Categories.Products")

                                 //.Include("Modules.Categories.Translation")
                                 .Include("Modules.Categories.CategoryProducts")
                             where c.CatalogId == id && c.Active == true
                             select c;
            var cata = catalogues.FirstOrDefault();

            CategoryRepository categoryRepo = new CategoryRepository(this.Context);
            ProductRepository prodRepo = new ProductRepository(this.Context);
                foreach (Module m in cata.Modules)
                {
                    foreach (Category p in m.Categories)
                    {
                        //foreach(CategoryProduct cp in p.CategoryProducts)

                        p.Products = prodRepo.GetProductByIds(p.CategoryProducts);

                        //p.CategoryChildren = categoryRepo.GetCategoriesChildren(p.CategoryId);
                    }
                }
            
            //string json = "";
            //try
            //{
            //    json = JsonConvert.SerializeObject(cata, Formatting.Indented,
            //            new JsonSerializerSettings
            //            {
            //                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            //            });
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            ////return catalogues;
            //return JsonConvert.SerializeObject(cata, Formatting.Indented,
            //        new JsonSerializerSettings
            //        {
            //            PreserveReferencesHandling = PreserveReferencesHandling.Objects
            //        });

            return cata;
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