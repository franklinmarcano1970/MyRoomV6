using MyRoom.Model;
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
            var product = (from c in this.Context.Products.Include("Translation").Include("RelatedProducts")
                         where c.Id == id
                         select c).First();

            string json = JsonConvert.SerializeObject(product, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
            return json;
        }

        public override async System.Threading.Tasks.Task EditAsync(Product entity)
        {

            try
            {
                Product product = await this.GetByIdAsync(entity.Id);
                await this.DeleteAsync(product);
                await this.InsertAsync(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeleteProductRealted(int productId)
        {
            RelatedProductRepository relatedProductRepo = new RelatedProductRepository(this.Context);

            List<RelatedProduct> relatedProduct = this.Context.RelatedProducts.Where(c => c.IdProduct == productId).ToList();
            try
            {
                relatedProductRepo.DeleteCollection(relatedProduct);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public MyRoomDbContext Context { get; private set; }
    }
}