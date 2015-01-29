using MyRoom.Helpers;
using MyRoom.Model;
using MyRoom.Model.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRoom.Data.Repositories
{
    public class CatalogRepository : GenericRepository<Catalog>
    {
        public CatalogRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }

        public string GetStructureComplete(int id)
        {
            var catalogues = from c in this.Context.Catalogues
                                 .Include("Translation")
                                 .Include("Modules")
                                 .Include("Modules.Translation")
                                 .Include("Modules.Categories")
                             where c.CatalogId == id && c.Active == true
                             select c;
            var cata = catalogues.FirstOrDefault();

            ProductRepository prodRepo = new ProductRepository(this.Context);
            IList<ModuleCompositeViewModel> modules = new List<ModuleCompositeViewModel>();

            foreach (Module m in cata.Modules)
            {
                ModuleCompositeViewModel moduleVm = Helper.ConvertModuleToViewModel(m);
                modules.Add(moduleVm);

                foreach (Category p in m.Categories)
                {
                    if (moduleVm.Children == null)
                        moduleVm.Children = new List<CategoryCompositeViewModel>();

                    CategoryCompositeViewModel category = Helper.ConvertCategoryToViewModel(p);
                    moduleVm.Children.Add(category);
                    CreateSubCategories(category);

                }

            }

            string json = "";
            try
            {
                json = JsonConvert.SerializeObject(modules, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                            ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                        });

                json = json.Replace("Children", "children");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return json;
        }

        //private CategoryCompositeViewModel CreateCategory(Category p)
        //{
        //    CategoryRepository categoryRepo = new CategoryRepository(this.Context);

        //    Category category = categoryRepo.GetById(p.CategoryId);

        //    CategoryCompositeViewModel categoryVm = Helper.ConvertCategoryToViewModel(category);

        //    return categoryVm;
        //}

        private List<CategoryCompositeViewModel> CreateSubCategories(CategoryCompositeViewModel p)
        {
            CategoryRepository categoryRepo = new CategoryRepository(this.Context);

            List<Category> categories = categoryRepo.GetByParentId(p.CategoryId);
            List<CategoryCompositeViewModel> categoriesVm = new List<CategoryCompositeViewModel>();
            categoriesVm.Add(p);
            foreach (Category c in categories)
            {
                CategoryCompositeViewModel categoryCompositeViewModel = Helper.ConvertCategoryToViewModel(c);
                if (p.Children == null)
                    p.Children = new List<CategoryCompositeViewModel>();
                p.Children.Add(categoryCompositeViewModel);
                if (categories != null)
                    CreateSubCategories(categoryCompositeViewModel);
                
            }
            
            return categoriesVm;
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