using MyRoom.Model;
using MyRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Mappers
{
    public class ActiveHotelProductsMapper
    {
        public static List<ActiveHotelProduct> CreateModel(AssignHotelCatalogViewModel assignHotelCatalogViewModel)
        {
            List<ActiveHotelProduct> products = new List<ActiveHotelProduct>();
            foreach (AssignHotelCatalog catalog in assignHotelCatalogViewModel.HotelCatalog)
            {
                if (catalog.Type == "product")
                {
                    products.Add(new ActiveHotelProduct()
                    {
                        IdHotel = assignHotelCatalogViewModel.HotelId,
                        IdProduct = catalog.ElementId,
                        Active = true
                    });
                }
            }
            return products;
        }
    }
}