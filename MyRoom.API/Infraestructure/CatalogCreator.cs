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
        private ProductRepository productRepo;

        public CatalogCreator(MyRoomDbContext context)
        {
            this.Context = context;
            productRepo = new ProductRepository(context);
        }

        public string CreateWithOutProducts(Catalog cata, bool activemod, bool activecategory, int hotelId = 0)
        {
            IList<ModuleCompositeViewModel> modules = new List<ModuleCompositeViewModel>();

            foreach (Module m in cata.Modules)
            {
                ModuleCompositeViewModel moduleVm = Helper.ConvertModuleToViewModel(m, activemod);
                modules.Add(moduleVm);

                foreach (Category p in m.Categories)
                {
                    if (moduleVm.Children == null)
                        moduleVm.Children = new List<CategoryCompositeViewModel>();

                    CategoryCompositeViewModel category = Helper.ConvertCategoryToViewModel(p);
                    if (activecategory)
                    {

                        category.IsChecked = p.ActiveHotelCategory.Contains(new ActiveHotelCategory() { IdCategory = p.CategoryId, IdHotel = hotelId, Active = true });
                        category.ActiveCheckbox = true;
                    }
                    moduleVm.Children.Add(category);
                    CreateSubCategories(category, false, activecategory, hotelId);

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

        public string CreateWithProducts(Catalog cata, bool activemod, bool activecategory, int hotelId = 0)
        {
            IList<ModuleCompositeViewModel> modules = new List<ModuleCompositeViewModel>();

            foreach (Module m in cata.Modules)
            {
                ModuleCompositeViewModel moduleVm = Helper.ConvertModuleToViewModel(m, activemod);
                modules.Add(moduleVm);
                moduleVm.ActiveCheckbox = activemod;
                m.ActiveHotelModule.ForEach(delegate(ActiveHotelModule hotelModule)
                {
                    if (hotelModule.IdHotel == hotelId && hotelModule.IdModule == m.ModuleId)
                    {
                        moduleVm.IsChecked = true;
                    }
                });
                foreach (Category p in m.Categories)
                {
                    if (moduleVm.Children == null)
                        moduleVm.Children = new List<CategoryCompositeViewModel>();

                    CategoryCompositeViewModel category = Helper.ConvertCategoryToViewModel(p);
                    // category.Products = new List<ProductCompositeViewModel>();
                    category.Children = new List<ICatalogChildren>();
                    if (activecategory)
                    {
                        p.ActiveHotelCategory.ForEach(delegate(ActiveHotelCategory hotelCategory)
                        {
                            if (hotelCategory.IdHotel == hotelId && hotelCategory.IdCategory == p.CategoryId)
                            {
                                category.IsChecked = true;
                            }
                        });

                        category.ActiveCheckbox = true;
                    }
                    foreach (CategoryProduct cp in p.CategoryProducts)
                    {
                        Product product = productRepo.GetById(cp.IdProduct);

                        ProductCompositeViewModel productVm =  new ProductCompositeViewModel()
                        {
                            ProductId = product.Id,
                            text = product.Name,
                            ActiveCheckbox = true,
                        };

                        product.ActiveHotelProduct.ForEach(delegate(ActiveHotelProduct activeProduct)
                        {
                            if (activeProduct.IdProduct == product.Id)
                                productVm.IsChecked = true;

                        });

                        category.Children.Add(productVm);
                    }

                    moduleVm.Children.Add(category);
                    CreateSubCategories(category, true, activecategory, hotelId);
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

                json = json.Replace("Children", "children").Replace("Products", "children");

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return json;

        }

        private List<CategoryCompositeViewModel> CreateSubCategories(CategoryCompositeViewModel p, bool withproducts, bool activecategory, int hotelId)
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

                if (activecategory)
                {
                    //   categoryCompositeViewModel.IsChecked = c.ActiveHotelCategory.Contains(new ActiveHotelCategory() { IdCategory = c.CategoryId, IdHotel = hotelId, Active = true, Category = c});
                    c.ActiveHotelCategory.ForEach(delegate(ActiveHotelCategory hotelCategory)
                    {
                        if (hotelCategory.IdHotel == hotelId && hotelCategory.Category.IdParentCategory == p.CategoryId)
                        {
                            categoryCompositeViewModel.IsChecked = true;
                        }
                    });

                    categoryCompositeViewModel.ActiveCheckbox = true;
                }

                if (withproducts)
                {
                    categoryCompositeViewModel.Children = new List<ICatalogChildren>();
                    foreach (CategoryProduct cp in c.CategoryProducts)
                    {
                        Product product = productRepo.GetById(cp.IdProduct);
                            ProductCompositeViewModel productViewModel = new ProductCompositeViewModel()
                            {
                                ProductId = product.Id,
                                text = product.Name,
                                ActiveCheckbox = true
                            };

                            categoryCompositeViewModel.Children.Add(productViewModel);

                            product.ActiveHotelProduct.ForEach(delegate(ActiveHotelProduct productHotelAct)
                            {
                                if (productHotelAct.IdHotel == hotelId && cp.IdProduct == productHotelAct.IdProduct )
                                {
                                    productViewModel.IsChecked = true;
                                }
                            });

                  
                    }
                }

                p.Children.Add(categoryCompositeViewModel);
                if (categories != null)
                    CreateSubCategories(categoryCompositeViewModel, withproducts, activecategory, hotelId);

            }

            return categoriesVm;
        }

        public MyRoomDbContext Context { get; private set; }

    }
}