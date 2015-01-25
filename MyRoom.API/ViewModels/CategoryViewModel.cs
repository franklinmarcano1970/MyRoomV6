using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.API.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public int? IdParentCategory { get; set; }

        public int? CategoryItem { get; set; }

        public bool IsFirst { get; set; }

        public bool IsFinal { get; set; }

        public bool CategoryActive { get; set; }

        public string Comment { get; set; }

        public int? Orden { get; set; }

        public bool? Pending { get; set; }

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

        public int ModuleId { get; set; }

        public string ModuleName { get; set; }
    }
}