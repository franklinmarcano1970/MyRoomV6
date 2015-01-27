using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Model.ViewModels
{
    public class CategoryViewModel  : BaseViewModel
    {
        public int CategoryId { get; set; }

        public int ModuleId { get; set; }

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

        public string ModuleName { get; set; }
    }
}