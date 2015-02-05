using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Model.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public string Type { get; set; }

        public bool Active { get; set; }

        public string Prefix { get; set; }

        public string Name_ENG { get; set; }

        public string Description_ENG { get; set; }

        public string UrlScanDocument { get; set; }

        public bool? Pending { get; set; }

        public int? Order { get; set; }

        public ICollection<RelatedProduct> RelatedProducts { get; set; }

    }


}