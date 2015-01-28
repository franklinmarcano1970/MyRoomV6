using MyRoom.Model.ViewModels;
using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Mappers
{
    public static class CategoryMapper
    {
        public static Category CreateModel(CategoryViewModel categoryViewModel)
        {
            Category category = new Category()
            {
                Active = categoryViewModel.CategoryActive,
                Name = categoryViewModel.Name,
                IdParentCategory = categoryViewModel.IdParentCategory,
                Orden = categoryViewModel.Orden,
                Image = categoryViewModel.Image,
                Pending = categoryViewModel.Pending,
                Prefix = categoryViewModel.Prefix,
                Comment = categoryViewModel.Comment,
                IsFinal = categoryViewModel.IsFinal,
                IsFirst = categoryViewModel.IsFirst
            };

            category.Translation = new Translation()
            {
                Spanish = categoryViewModel.Spanish,
                English = categoryViewModel.English,
                French = categoryViewModel.French,
                German = categoryViewModel.German,
                Language5 = categoryViewModel.Language5,
                Language6 = categoryViewModel.Language6,
                Language7 = categoryViewModel.Language7,
                Language8 = categoryViewModel.Language8,
                Active = categoryViewModel.TranslationActive,
            };
            category.Modules = new List<Module>();

            category.Modules.Add(new Module()
            {
                ModuleId = categoryViewModel.ModuleId,
                Name = categoryViewModel.ModuleName
            });
            return category;
        }
    }
}