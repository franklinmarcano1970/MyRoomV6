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

    public class ActiveHotelProductController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/ActiveHotelProduct
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<ActiveHotelProduct> GetActiveHotelProduct()
        {
            return db.ActiveHotelProduct;
        }

        // GET: odata/ActiveHotelProduct(5)
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<ActiveHotelProduct> GetActiveHotelProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.ActiveHotelProduct.Where(activeHotelProduct => activeHotelProduct.IdHotel == key));
        }

        // PUT: odata/ActiveHotelProduct(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<ActiveHotelProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActiveHotelProduct activeHotelProduct = await db.ActiveHotelProduct.FindAsync(key);
            if (activeHotelProduct == null)
            {
                return NotFound();
            }

            patch.Put(activeHotelProduct);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveHotelProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(activeHotelProduct);
        }

        // POST: odata/ActiveHotelProduct
        public async Task<IHttpActionResult> Post(ActiveHotelProduct activeHotelProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActiveHotelProduct.Add(activeHotelProduct);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ActiveHotelProductExists(activeHotelProduct.IdHotel))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(activeHotelProduct);
        }

        // PATCH: odata/ActiveHotelProduct(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ActiveHotelProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActiveHotelProduct activeHotelProduct = await db.ActiveHotelProduct.FindAsync(key);
            if (activeHotelProduct == null)
            {
                return NotFound();
            }

            patch.Patch(activeHotelProduct);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveHotelProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(activeHotelProduct);
        }

        // DELETE: odata/ActiveHotelProduct(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            ActiveHotelProduct activeHotelProduct = await db.ActiveHotelProduct.FindAsync(key);
            if (activeHotelProduct == null)
            {
                return NotFound();
            }

            db.ActiveHotelProduct.Remove(activeHotelProduct);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ActiveHotelProduct(5)/Hotels
        [EnableQuery]
        public SingleResult<Hotel> GetHotels([FromODataUri] int key)
        {
            return SingleResult.Create(db.ActiveHotelProduct.Where(m => m.IdHotel == key).Select(m => m.Hotel));
        }

        // GET: odata/ActiveHotelProduct(5)/Products
        [EnableQuery]
        public SingleResult<Product> GetProducts([FromODataUri] int key)
        {
            return SingleResult.Create(db.ActiveHotelProduct.Where(m => m.IdHotel == key).Select(m => m.Product));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActiveHotelProductExists(int key)
        {
            return db.ActiveHotelProduct.Count(e => e.IdHotel == key) > 0;
        }
    }
}
