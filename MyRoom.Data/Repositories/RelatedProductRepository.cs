using MyRoom.Model;
using MyRoom.ViewModels;
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

        public List<RelatedProductViewModel> GetActiveProductRelated(int hotelId, int prodId=0)
        {
            List<RelatedProductViewModel> relatedProds = new List<RelatedProductViewModel>();

            var products = this.Context.ActiveHotelProduct.Where(e => e.IdHotel == hotelId).Include("Product").Include("Product.RelatedProducts").ToList();

            var prodRelated = this.GetProductRelated(prodId).ToList();
            foreach(ActiveHotelProduct prod in products)
            {
                if (prodId != prod.IdProduct)
                { 
                    relatedProds.Add(new RelatedProductViewModel() {  
                        Id = prod.Product.Id, 
                        Active = prod.Product.Active, 
                        Name  = prod.Product.Name, 
                        Price = prod.Product.Price ,
                        Checked = prodRelated.Where(e=>e.IdRelatedProduct == prod.IdProduct).Any()
                    });
                }
            }

     
            return relatedProds;
        }
    }
}