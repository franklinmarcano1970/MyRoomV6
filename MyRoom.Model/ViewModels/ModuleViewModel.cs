using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Model.ViewModels
{
    public class ModuleViewModel  : BaseViewModel
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public bool ModuleActive { get; set; }

        public string Comment { get; set; }

        public bool? Pending { get; set; }

        public int? Orden { get; set; }

        public string Prefix { get; set; }

        public int CatalogId { get; set; }

        public string CatalogName { get; set; }

    }
}