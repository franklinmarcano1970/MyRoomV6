using MyRoom.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Repositories
{
    public class RelatedProductRepository : GenericRepository<RelatedProduct>
    {
        public RelatedProductRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }


        public void DeleteProductRealted(int productId)
        {

            List<RelatedProduct> relatedProduct = this.Context.RelatedProducts.Where(c => c.IdProduct == productId).ToList();
            try
            {
                this.DeleteCollection(relatedProduct);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MyRoomDbContext Context { get; private set; }
    }
}