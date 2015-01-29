using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Model.ViewModels
{
    public class ModuleCompositeViewModel
    {
        public string text { get; set; }

        public string Prefix { get; set; }

        public string type { get; set; }

        public int ModuleId { get; set; }

        public int IdTranslationName { get; set; }
        
        public string Name { get; set; }

        public string Image { get; set; }

        public int? Orden { get; set; }

        public string Comment { get; set; }

        public bool? Pending { get; set; }

        public bool Active { get; set; }
        
        public string nextsibling { get; set; }

        public Translation Translation { get; set; }

        public List<CategoryCompositeViewModel> Children { get; set; }

    }
}