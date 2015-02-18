using MyRoom.Model;
using MyRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Mappers
{
    public class ActiveHotelModulesMapper
    {
        public static List<ActiveHotelModule> CreateModel(AssignHotelCatalogViewModel assignHotelCatalogViewModel)
        {
            List<ActiveHotelModule> modules = new List<ActiveHotelModule>();
            foreach (AssignHotelCatalog catalog in assignHotelCatalogViewModel.HotelCatalog)
            {
                if (catalog.Type == "module")
                {
                    modules.Add(new ActiveHotelModule()
                    {
                        IdHotel = assignHotelCatalogViewModel.HotelId,
                        IdModule = catalog.ElementId,
                        Active = true
                    });
                }
            }
            return modules;
        }
    }
}