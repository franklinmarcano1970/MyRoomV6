using MyRoom.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRoom.Data
{
    public class CatalogLeaf<TEntity> : CatalogComponent<TEntity>
    {
        public CatalogLeaf(TEntity entity) : base(entity)
        {

        }
    }
}