using MyRoom.Model.ViewModels;
using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Mappers
{
    public static class ModuleMapper
    {
        public static Module CreateModel(ModuleViewModel moduleViewModel)
        {
            Module module = new Module()
            {
                Active = moduleViewModel.ModuleActive,
                Name = moduleViewModel.Name,
                Orden = moduleViewModel.Orden,
                Image = moduleViewModel.Image,
                Pending = moduleViewModel.Pending,
                Prefix = moduleViewModel.Prefix,
                Comment = moduleViewModel.Comment
            };

            module.Translation = new Translation()
            {
                Spanish = moduleViewModel.Spanish,
                English = moduleViewModel.English,
                French = moduleViewModel.French,
                German = moduleViewModel.German,
                Language5 = moduleViewModel.Language5,
                Language6 = moduleViewModel.Language6,
                Language7 = moduleViewModel.Language7,
                Language8 = moduleViewModel.Language8,
                Active = moduleViewModel.TranslationActive,
            };
            module.Catalogues = new List<Catalog>();

            module.Catalogues.Add(new Catalog()
            {
                CatalogId = moduleViewModel.CatalogId,
                Name = moduleViewModel.CatalogName
            });

         
            return module;
        }
    }
}