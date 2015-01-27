using MyRoom.Model;
using MyRoom.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Helpers
{
    public abstract class CatalogComponent
    {
        public CatalogCompositeViewModel Catalog { private set; get; }

        public CatalogComponent(CatalogCompositeViewModel entity)
        {
            this.Catalog = entity;
        }

    }
}