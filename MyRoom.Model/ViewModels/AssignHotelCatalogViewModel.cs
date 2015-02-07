using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRoom.ViewModels
{
    public class AssignHotelCatalogViewModel
    {
        private List<AssignHotelCatalog> hotelCatalog;
        public AssignHotelCatalogViewModel()
        {
            hotelCatalog = new List<AssignHotelCatalog>();
        }

        public List<AssignHotelCatalog> HotelCatalog { get; set; }
    }

    public class AssignHotelCatalog
    {
        public int HotelId { get; set; }

        //Product, Category or Module
        public int ElementId { get; set; }
        public string Type { get; set; }
    }

}
