using System;
using System.Security.Cryptography;
using MyRoom.Model;
using MyRoom.Model.ViewModels;
using Newtonsoft.Json;
using MyRoom.Data.Repositories;
using System.Collections.Generic;

namespace MyRoom.Helpers
{
    public class Helper
    {
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static ModuleCompositeViewModel ConvertModuleToViewModel(Module module)
        {
            ModuleCompositeViewModel  moduleCompositeViewModel = new ModuleCompositeViewModel()
            {
                text = module.Name,
                Prefix = module.Prefix,
                type = "module",
                ModuleId = module.ModuleId,
                IdTranslationName = module.IdTranslationName,
                Image = module.Image,
                Name = module.Name,
                Orden = module.Orden,
                Comment = module.Comment,
                Pending = module.Pending,
                Active = module.Active,
                nextsibling = "category",
                Translation = module.Translation
            };

             return moduleCompositeViewModel;            
        }

        public static CatalogCompositeViewModel ConvertCatalogToViewModel(Catalog catalog)
        {
            CatalogCompositeViewModel moduleCompositeViewModel = new CatalogCompositeViewModel()
            {
                 CatalogId = catalog.CatalogId,
                 Name = catalog.Name
            };

            return moduleCompositeViewModel;

        }


        public static CategoryCompositeViewModel ConvertCategoryToViewModel(Category category)
        {
            CategoryCompositeViewModel categoryCompositeViewModel = new CategoryCompositeViewModel()
            {

                CategoryId = category.CategoryId,
                text = category.Name,
                Prefix = category.Prefix,
                type = "category",
                IdParentCategory = category.IdParentCategory,
                IdTranslationName = category.IdTranslationName,
                Image = category.Image,
                IsFinal = category.IsFinal,
                IsFirst = category.IsFirst,
                Name = category.Name,
                Orden = category.Orden,
                Comment = category.Comment,
                Pending = category.Pending,
                Active = category.Active,
                nextsibling = "category",
                Translation = category.Translation
            };
        
            return categoryCompositeViewModel;
        }
    }
}