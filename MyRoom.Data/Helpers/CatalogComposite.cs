using MyRoom.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Helpers
{
    public class CatalogComposite : CatalogComponent
    {
        private IList<CatalogComponent> nodes;

        public CatalogComposite(CatalogCompositeViewModel entity)
            : base(entity)
        {
            nodes = new List<CatalogComponent>();
        }

        public void AddModule(CatalogComponent c)
        { 
            nodes.Add(c);
        }

        public void RemoveNode(CatalogComponent c)
        {
            nodes.Remove(c);
        }
    }
}