using MyRoom.Model;
using System.Collections.Generic;
using System.Linq;

namespace CodeCamper.Data.SampleData
{
    public class SampleCatalogue
    {
        public static List<SampleCatalogue> Catalogue =
            new List<SampleCatalogue>
                {
                   new SampleCatalogue("Empresarial Catalogue","catalogue1.jpg", true),
                };

    
     
        public SampleCatalogue(string name, string codeRoot, params string[] tags)
        {
            Name = name;
            CodeRoot = codeRoot;
            Tags = tags.ToList();
        }

        public string Name { get; set; }
       

    }
}