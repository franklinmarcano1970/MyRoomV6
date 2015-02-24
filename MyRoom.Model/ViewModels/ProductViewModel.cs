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

        public string UrlScanDocument { get; set; }

        public bool? Pending { get; set; }

        public int? Order { get; set; }

        public string SpanishDesc { get; set; }

        public string EnglishDesc { get; set; }

        public string FrenchDesc { get; set; }

        public string GermanDesc { get; set; }

        public bool TranslationActiveDesc { get; set; }

        public string LanguageDesc5 { get; set; }

        public string LanguageDesc6 { get; set; }

        public string LanguageDesc7 { get; set; }

        public string LanguageDesc8 { get; set; }

        public int CatalogId { get; set; }

        public ICollection<RelatedProduct> RelatedProducts { get; set; }

    }


}