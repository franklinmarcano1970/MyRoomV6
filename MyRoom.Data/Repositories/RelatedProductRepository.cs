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

            List<RelatedProduct> relatedProduct = (from p in this.Context.RelatedProducts
                                                    where p.IdProduct == productId
                                                       select p).ToList();
            try
            {
                this.DeleteCollection(relatedProduct);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertRelatedProducts(List<RelatedProduct> productsrelated)
        {
            productsrelated.ForEach(delegate(RelatedProduct product)
            {
                this.Insert(product);
            });

        }    

        public MyRoomDbContext Context { get; private set; }

        public IQueryable<RelatedProduct> GetProductRelated(int prodId)
        {
            return  this.Context.RelatedProducts.Where(e=>e.IdProduct==prodId);
        }
    }
}