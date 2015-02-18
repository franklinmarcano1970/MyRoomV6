using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRoom.Model.ViewModels
{
    public class ProductCompositeViewModel  : ICatalogChildren
    {
        public ProductCompositeViewModel()
        {
            this.type = "product";
        }

        public int ProductId { get; set; }
        public bool ActiveCheckbox { get; set; }
        public bool IsChecked { get; set; }
        public string type { get; set; }
        public string text{ get; set; }
    }
}
