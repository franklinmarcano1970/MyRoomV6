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

namespace MyRoom.API.Controllers
{

    public class RelatedProductsController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/RelatedProducts
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<RelatedProduct> GetRelatedProducts()
        {
            return db.RelatedProducts;
        }

        // GET: odata/RelatedProducts(5)
        //[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        //public SingleResult<RelatedProduct> GetRelatedProducts([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.RelatedProducts.Where(relatedProducts => relatedProducts.Id == key));
        //}

        // PUT: odata/RelatedProducts(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<RelatedProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelatedProduct relatedProduct = await db.RelatedProducts.FindAsync(key);
            if (relatedProduct== null)
            {
                return NotFound();
            }

            patch.Put(relatedProduct);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelatedProductsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relatedProduct);
        }

        // POST: odata/RelatedProducts
        public async Task<IHttpActionResult> Post(RelatedProduct relatedProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RelatedProducts.Add(relatedProduct);
            await db.SaveChangesAsync();

            return Created(relatedProduct);
        }

        // PATCH: odata/RelatedProducts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<RelatedProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelatedProduct relatedProducts = await db.RelatedProducts.FindAsync(key);
            if (relatedProducts == null)
            {
                return NotFound();
            }

            patch.Patch(relatedProducts);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelatedProductsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relatedProducts);
        }

        // DELETE: odata/RelatedProducts(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            RelatedProduct relatedProduct = await db.RelatedProducts.FindAsync(key);
            if (relatedProduct == null)
            {
                return NotFound();
            }

            db.RelatedProducts.Remove(relatedProduct);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        //// GET: odata/RelatedProducts(5)/Products
        //[EnableQuery]
        //public SingleResult<Product> GetProducts([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.RelatedProducts.Where(m => m.Id == key).Select(m => m.Product));
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RelatedProductsExists(int key)
        {
            return db.RelatedProducts.Count(e => e.IdRelatedProduct == key) > 0;
        }
    }
}
