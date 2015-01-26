using MyRoom.Model.ViewModels;
using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyRoom.Data.Repositories;

namespace MyRoom.Data.Mappers
{
    public static class ProductMapper
    {
        public static Product CreateModel(ProductViewModel productViewModel)
        {
            Product product = new Product()
            {
                Name            = productViewModel.Name,
                Description     = productViewModel.Description,
                Price           = productViewModel.Price,
                Image           = productViewModel.Image,
                Type            = productViewModel.Type,
                Active          = productViewModel.ProductActive,
                Prefix          = productViewModel.Prefix,
                Name_ENG        = productViewModel.Name_ENG,                
                Description_ENG = productViewModel.Description_ENG,
                UrlScanDocument = productViewModel.UrlScanDocument,
                Pending         = productViewModel.Pending,
                Order           = productViewModel.Order              
            };

            product.Translation = new Translation()
            {
                Spanish = productViewModel.Spanish,
                English = productViewModel.English,
                French = productViewModel.French,
                German = productViewModel.German,
                Language5 = productViewModel.Language5,
                Language6 = productViewModel.Language6,
                Language7 = productViewModel.Language7,
                Language8 = productViewModel.Language8,
                Active = productViewModel.TranslationActive,
            };

            return product;
        }
    }
}