using MyRoom.Model;
using MyRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Mappers
{
    public class UserModuleMapper
    {
        public static List<RelUserModule> CreateModel(UserCatalogViewModel userCatalogViewModel)
        {
            List<RelUserModule> userModules = new  List<RelUserModule>();
            
            foreach(UserCatalog userCatalog in userCatalogViewModel.Elements)
            {
                if (userCatalog.Type == "module")
                    userModules.Add(new RelUserModule { 
                            IdUser = userCatalogViewModel.UserId, 
                            IdModule = userCatalog.id 
                    });
            }
            return userModules;
        }

    }
}