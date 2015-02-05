using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRoom.Model.ViewModels
{
    public class ProductCompositeViewModel  : ICatalogChildren
    {
        public int ProductId { get; set; }
        public bool ActiveCheckbox { get; set; }
        public string text{ get; set; }
    }
}
