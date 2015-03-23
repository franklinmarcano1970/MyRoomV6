using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRoom.ViewModels
{
    public class RelatedProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public bool Checked { get; set; }
    }
}
