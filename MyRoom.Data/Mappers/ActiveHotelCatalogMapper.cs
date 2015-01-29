using MyRoom.Model;
using MyRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Mappers
{
    public class ActiveHotelCatalogMapper
    {
        public static List<ActiveHotelCatalogue> CreateModel(ActiveHotelCataloguesViewModel hotelCatalogViewModel)
        {
            List<ActiveHotelCatalogue> hotelCatalogues = new List<ActiveHotelCatalogue>();
            foreach (int catalogid in hotelCatalogViewModel.CataloguesIds)
            {
                hotelCatalogues.Add(new ActiveHotelCatalogue()
                {
                    IdHotel = hotelCatalogViewModel.HotelId,
                    IdCatalogue = catalogid,
                    Active = true
                });
            }
            return hotelCatalogues;
        }
    }
}