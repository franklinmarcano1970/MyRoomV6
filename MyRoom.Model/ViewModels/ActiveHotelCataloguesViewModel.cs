using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRoom.ViewModels
{
    public class ActiveHotelCataloguesViewModel
    {
        public int HotelId { get; set; }
        public List<int> CataloguesIds { get; set; }
    }
}
