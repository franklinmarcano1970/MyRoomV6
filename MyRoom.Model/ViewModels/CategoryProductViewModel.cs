using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRoom.ViewModels
{
    public class CategoryProductViewModel
    {
        public int CategoryId { get; set; }
        public ICollection<int> ProductsIds { get; set; }
    }
}
