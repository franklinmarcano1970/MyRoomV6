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

    public class RelUserProductsController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/RelUserProducts
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<RelUserProduct> GetRelUserProducts()
        {
            return db.RelUserProduct;
        }

        // GET: odata/RelUserProducts(5)
        [EnableQuery( AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<RelUserProduct> GetRelUserProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.RelUserProduct.Where(relUserProduct => relUserProduct.Id == key));
        }

        // PUT: odata/RelUserProducts(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<RelUserProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelUserProduct relUserProduct = await db.RelUserProduct.FindAsync(key);
            if (relUserProduct == null)
            {
                return NotFound();
            }

            patch.Put(relUserProduct);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelUserProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relUserProduct);
        }

        // POST: odata/RelUserProducts
        public async Task<IHttpActionResult> Post(RelUserProduct relUserProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RelUserProduct.Add(relUserProduct);
            await db.SaveChangesAsync();

            return Created(relUserProduct);
        }

        // PATCH: odata/RelUserProducts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<RelUserProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelUserProduct relUserProduct = await db.RelUserProduct.FindAsync(key);
            if (relUserProduct == null)
            {
                return NotFound();
            }

            patch.Patch(relUserProduct);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelUserProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relUserProduct);
        }

        // DELETE: odata/RelUserProducts(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            RelUserProduct relUserProduct = await db.RelUserProduct.FindAsync(key);
            if (relUserProduct == null)
            {
                return NotFound();
            }

            db.RelUserProduct.Remove(relUserProduct);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/RelUserProducts(5)/Products
        [EnableQuery]
        public SingleResult<Product> GetProducts([FromODataUri] int key)
        {
            return SingleResult.Create(db.RelUserProduct.Where(m => m.Id == key).Select(m => m.Product));
        }

        // GET: odata/RelUserProducts(5)/User
        //[EnableQuery]
        //public SingleResult<User> GetUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.RelUserProduct.Where(m => m.Id == key).Select(m => m.User));
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RelUserProductExists(int key)
        {
            return db.RelUserProduct.Count(e => e.Id == key) > 0;
        }
    }
}
