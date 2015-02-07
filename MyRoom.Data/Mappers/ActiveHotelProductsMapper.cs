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
                        IdProduct = catalog.ElementId,
                        IdHotel = catalog.HotelId,
                        Active = true
                    });
                }
            }
            return products;
        }
    }
}