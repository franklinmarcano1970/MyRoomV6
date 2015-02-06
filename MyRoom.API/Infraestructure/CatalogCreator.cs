using MyRoom.Data;
using MyRoom.Data.Repositories;
using MyRoom.Helpers;
using MyRoom.Model;
using MyRoom.Model.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MyRoom.API.Infraestructure
{
    public class CatalogCreator
    {
        public CatalogCreator(MyRoomDbContext context)
        {
            this.Context = context;
        }

        public string CreateWithOutProducts(Catalog cata)
        {
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

        public string CreateWithProducts(Catalog cata)
        {
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
                    ProductRepository productRepo = new ProductRepository(this.Context);
                   // category.Products = new List<ProductCompositeViewModel>();
                    category.Children = new List<ICatalogChildren>();
                    if (category.IsFinal)
                    {
                        category.ActiveCheckbox = true;
                    }
                    foreach(CategoryProduct cp in p.CategoryProducts)
                    {
                        Product product = productRepo.GetById(cp.IdProduct);                             
                        category.Children.Add(new ProductCompositeViewModel(){
                            ProductId = product.Id, 
                            text = product.Name,
                            ActiveCheckbox = true
                        });
                    }

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

                json = json.Replace("Children", "children").Replace("Products","children");

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return json;

        }

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
                    p.Children = new List<ICatalogChildren>();
                
                p.Children.Add(categoryCompositeViewModel);
                if (categories != null)
                    CreateSubCategories(categoryCompositeViewModel);

            }

            return categoriesVm;
        }

        public MyRoomDbContext Context { get; private set; }

    }
}