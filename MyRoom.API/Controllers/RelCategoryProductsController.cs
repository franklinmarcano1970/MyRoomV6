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
    public class RelCategoryProductsController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/RelCategoryProducts
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<RelCategoryProduct> GetRelCategoryProducts()
        {
            return db.RelCategoryProduct;
        }

        // GET: odata/RelCategoryProducts(5)
        //[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        //public SingleResult<RelCategoryProduct> GetRelCategoryProduct([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.RelCategoryProduct.Where(relCategoryProduct => relCategoryProduct.IdCategory == key));
        //}

        // PUT: odata/RelCategoryProducts(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<RelCategoryProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelCategoryProduct relCategoryProduct = await db.RelCategoryProduct.FindAsync(key);
            if (relCategoryProduct == null)
            {
                return NotFound();
            }

            patch.Put(relCategoryProduct);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelCategoryProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relCategoryProduct);
        }

        // POST: odata/RelCategoryProducts
        public async Task<IHttpActionResult> Post(RelCategoryProduct relCategoryProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RelCategoryProduct.Add(relCategoryProduct);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RelCategoryProductExists(relCategoryProduct.IdCategory))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(relCategoryProduct);
        }

        // PATCH: odata/RelCategoryProducts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<RelCategoryProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelCategoryProduct relCategoryProduct = await db.RelCategoryProduct.FindAsync(key);
            if (relCategoryProduct == null)
            {
                return NotFound();
            }

            patch.Patch(relCategoryProduct);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelCategoryProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relCategoryProduct);
        }

        // DELETE: odata/RelCategoryProducts(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            RelCategoryProduct relCategoryProduct = await db.RelCategoryProduct.FindAsync(key);
            if (relCategoryProduct == null)
            {
                return NotFound();
            }

            db.RelCategoryProduct.Remove(relCategoryProduct);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/RelCategoryProducts(5)/Categories
        //[EnableQuery]
        //public SingleResult<Categories> GetCategories([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.RelCategoryProduct.Where(m => m.IdCategory == key).Select(m => m.Categories));
        //}

        // GET: odata/RelCategoryProducts(5)/Products
        [EnableQuery]
        public IQueryable<Product> GetProducts([FromODataUri] int key)
        {
            return db.RelCategoryProduct.Where(m => m.IdCategory == key).Select(m => m.Product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RelCategoryProductExists(int key)
        {
            return db.RelCategoryProduct.Count(e => e.IdCategory == key) > 0;
        }
    }
}
