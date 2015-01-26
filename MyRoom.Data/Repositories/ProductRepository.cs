using MyRoom.Model;
using MyRoom.Model.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }

        public string GetProductById(int id)
        {
            var product = (from c in this.Context.Products.Include("Translation")
                           where c.Id == id
                           select c).First();

            RelatedProductRepository relprod = new RelatedProductRepository(this.Context);
            product.RelatedProducts = relprod.GetProductRelated(product.Id).ToList();

            string json = JsonConvert.SerializeObject(product, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects ,
                        ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                    });
            return json;
        }

        public ICollection<Product> GetProductByIds(ICollection<CategoryProduct> categoryProducts)
        {
            List<Product> products = new List<Product>();
            foreach (CategoryProduct cp in categoryProducts)
            {
                products.Add(this.GetById(cp.IdProduct));
            }
            return products;
        }

        //public void DeleteProductRealted(int productId)
        //{
        //    RelatedProductRepository relatedProductRepo = new RelatedProductRepository(this.Context);

        //    List<RelatedProduct> relatedProduct = this.Context.RelatedProducts.Where(c => c.IdProduct == productId).ToList();
        //    try
        //    {
        //        relatedProductRepo.DeleteCollection(relatedProduct);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public MyRoomDbContext Context { get; private set; }

        public List<Product> GetAll(List<RelatedProduct> relatedprods)
        {
            List<Product> products = new List<Product>();
            relatedprods.ForEach(delegate(RelatedProduct product)
            {
                Product prod = this.GetById(product.IdRelatedProduct);
                if(prod != null)
                    products.Add(prod);
            });
            return products;
        }

        public void StateUnchange(Product entity)
        {
            this.Context.Entry(entity).State = EntityState.Unchanged;
        }
      
    }
}