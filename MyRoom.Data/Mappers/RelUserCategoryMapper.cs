using MyRoom.Model;
using MyRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Mappers
{
    public class UserCategoryMapper
    {
        public static List<RelUserCategory> CreateModel(UserCatalogViewModel userCatalogViewModel)
        {
            List<RelUserCategory> userCategories = new  List<RelUserCategory>();
            
            foreach(UserCatalog userCatalog in userCatalogViewModel.Elements)
            {
                if (userCatalog.Type == "category")
                    userCategories.Add(new RelUserCategory { 
                            IdUser = userCatalogViewModel.UserId, 
                            IdCategory = userCatalog.id 
                    });
            }
            return userCategories;
        }

    }
}