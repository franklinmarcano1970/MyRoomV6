using MyRoom.Model;
using MyRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Mappers
{
    public class ActiveHotelCategoriesMapper
    {
        public static List<ActiveHotelCategory> CreateModel(AssignHotelCatalogViewModel assignHotelCatalogViewModel)
        {
            List<ActiveHotelCategory> categories = new List<ActiveHotelCategory>();
            foreach (AssignHotelCatalog catalog in assignHotelCatalogViewModel.HotelCatalog)
            {
                if (catalog.Type == "category")
                {
                    categories.Add(new ActiveHotelCategory()
                    {
                        IdCategory = catalog.ElementId,
                        IdHotel = catalog.HotelId,
                        Active = true
                    });
                }
            }
            return categories;
        }
    }
}