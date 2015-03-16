using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRoom.ViewModels
{
    public class UserCatalogViewModel
    {
        public string UserId { get; set; }

        public int CatalogId { get; set; }

        public List<UserCatalog> Elements { get; set; }
    }

    public class UserCatalog
    {
        //Category or Module
        public int id { get; set; }
        public string Type { get; set; }
    }

}
