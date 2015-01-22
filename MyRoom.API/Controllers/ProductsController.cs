using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using MyRoom.Model;
using System.Web.Http.OData.Query;
using MyRoom.Data;
using MyRoom.Data.Repositories;

namespace MyRoom.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        ProductRepository productRepository = new ProductRepository(new MyRoomDbContext());

        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return productRepository.GetAll().OrderBy(p => p.Name);
        }

        // GET: api/Products/5
        [Route("{key}")]
        [HttpGet]
        public string GetProducts(int key)
        {
            return productRepository.GetProductById(key);
        }

        // GET: odata/Products(5
        //[EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.All)]
        //public SingleResult<Product> GetProducts([FromODataUri] int key)
        //{
        //    //return SingleResult.Create(db.Products.Where(products => products.Id == key));
        //}

        // PUT: api/Products
        public async Task<IHttpActionResult> PutProducts(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await productRepository.EditAsync(product);
            }
            catch (Exception ex)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }

            return Ok("Product Updated");
        }

        private bool ProductExists(int key)
        {
            return productRepository.Context.Products.Count(e => e.Id == key) > 0;
        }

        // POST: api/Products
        public async Task<IHttpActionResult> PostProducts(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await productRepository.InsertAsync(product);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PATCH: odata/Products(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Product> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Product product = await _genericRepository.GetByIdAsync(key);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(product);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductsExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(product);
        //}

        // DELETE: api/Products/5
        [Route("{key}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProducts(int key)
        {
            Product product = await productRepository.GetByIdAsync(key);
            if (product == null)
            {
                return NotFound();
            }
            try
            {
                await productRepository.DeleteAsync(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Products(5)/ActiveHotelProduct
        //[EnableQuery]
        //public IQueryable<ActiveHotelProduct> GetActiveHotelProduct([FromODataUri] int key)
        //{
        //    return db.Products.Where(m => m.Id == key).SelectMany(m => m.ActiveHotelProduct);
        //}

        //// GET: odata/Products(5)/RelatedProducts
        //[EnableQuery]
        //public IQueryable<RelatedProducts> GetRelatedProducts([FromODataUri] int key)
        //{
        //    return db.Products.Where(m => m.Id == key).SelectMany(m => m.RelatedProducts);
        //}

        //// GET: odata/Products(5)/RelCategoryProduct
        //[EnableQuery]
        //public IQueryable<RelCategoryProduct> GetRelCategoryProduct([FromODataUri] int key)
        //{
        //    return db.Products.Where(m => m.Id == key).SelectMany(m => m.RelCategoryProduct);
        //}

        //// GET: odata/Products(5)/RelUserProduct
        //[EnableQuery]
        //public IQueryable<RelUserProduct> GetRelUserProduct([FromODataUri] int key)
        //{
        //    return db.Products.Where(m => m.Id == key).SelectMany(m => m.RelUserProduct);
        //}

        //// GET: odata/Products(5)/Translations
        //[EnableQuery]
        //public SingleResult<Translation> GetTranslations([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.Products.Where(m => m.Id == key).Select(m => m.Translation));
        //}

        //// GET: odata/Products(5)/Translations1
        //[EnableQuery]
        //public SingleResult<Translation> GetTranslations1([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.Products.Where(m => m.Id == key).Select(m => m.Translation1));
        //}


        //private bool ProductsExists(int key)
        //{

        //    return db.Products.Count(e => e.Id == key) > 0;
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                productRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
