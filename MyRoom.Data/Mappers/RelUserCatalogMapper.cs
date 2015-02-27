using MyRoom.Model;
using MyRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Mappers
{
    public class UserCatalogMapper
    {
        public static RelUserCatalogue CreateModel(UserCatalogViewModel userCatalogViewModel)
        {
            return new RelUserCatalogue { 
                   IdUser = userCatalogViewModel.UserId,
                    IdCatalogue = userCatalogViewModel.CatalogId,
                    ReadOnly = true,
                    ReadWrite  = false
            };
        }
    }
}