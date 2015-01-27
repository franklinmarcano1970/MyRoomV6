using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Model.ViewModels
{
    public class HotelCataloguesViewModel 
    {
        public int HotelId { get; set; }

        public ICollection<ActiveHotelCatalogue> CataloguesActives { get; set; }
    }
}