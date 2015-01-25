using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.API.ViewModels
{
    public class ModuleViewModel
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public bool ModuleActive { get; set; }

        public string Comment { get; set; }

        public bool? Pending { get; set; }

        public int? Orden { get; set; }

        public string Prefix { get; set; }

        public string Spanish { get; set; }

        public string English { get; set; }

        public string French { get; set; }

        public string German { get; set; }

        public bool TranslationActive { get; set; }

        public string Language5 { get; set; }

        public string Language6 { get; set; }

        public string Language7 { get; set; }

        public string Language8 { get; set; }

        public int CatalogId { get; set; }

        public string CatalogName { get; set; }

    }
}