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

    public class ActiveHotelCatalogueController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/ActiveHotelCatalogue
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<ActiveHotelCatalogue> GetActiveHotelCatalogue()
        {
            return db.ActiveHotelCatalogue;
        }

        // GET: odata/ActiveHotelCatalogue(5)
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<ActiveHotelCatalogue> GetActiveHotelCatalogue([FromODataUri] int key)
        {
            return SingleResult.Create(db.ActiveHotelCatalogue.Where(activeHotelCatalogue => activeHotelCatalogue.IdHotel == key));
        }

        // PUT: odata/ActiveHotelCatalogue(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<ActiveHotelCatalogue> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActiveHotelCatalogue activeHotelCatalogue = await db.ActiveHotelCatalogue.FindAsync(key);
            if (activeHotelCatalogue == null)
            {
                return NotFound();
            }

            patch.Put(activeHotelCatalogue);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveHotelCatalogueExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(activeHotelCatalogue);
        }

        // POST: odata/ActiveHotelCatalogue
        public async Task<IHttpActionResult> Post(ActiveHotelCatalogue activeHotelCatalogue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActiveHotelCatalogue.Add(activeHotelCatalogue);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ActiveHotelCatalogueExists(activeHotelCatalogue.IdHotel))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(activeHotelCatalogue);
        }

        // PATCH: odata/ActiveHotelCatalogue(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ActiveHotelCatalogue> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActiveHotelCatalogue activeHotelCatalogue = await db.ActiveHotelCatalogue.FindAsync(key);
            if (activeHotelCatalogue == null)
            {
                return NotFound();
            }

            patch.Patch(activeHotelCatalogue);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveHotelCatalogueExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(activeHotelCatalogue);
        }

        // DELETE: odata/ActiveHotelCatalogue(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            ActiveHotelCatalogue activeHotelCatalogue = await db.ActiveHotelCatalogue.FindAsync(key);
            if (activeHotelCatalogue == null)
            {
                return NotFound();
            }

            db.ActiveHotelCatalogue.Remove(activeHotelCatalogue);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ActiveHotelCatalogue(5)/Catalogues
        [EnableQuery]
        public IQueryable<Catalog> GetCatalogues([FromODataUri] int key)
        {
            return db.ActiveHotelCatalogue.Where(m => m.IdHotel == key).Select(m => m.Catalog);
        }

        // GET: odata/ActiveHotelCatalogue(5)/Hotels
        [EnableQuery]
        public SingleResult<Hotel> GetHotels([FromODataUri] int key)
        {
            return SingleResult.Create(db.ActiveHotelCatalogue.Where(m => m.IdHotel == key).Select(m => m.Hotel));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActiveHotelCatalogueExists(int key)
        {
            return db.ActiveHotelCatalogue.Count(e => e.IdHotel == key) > 0;
        }
    }
}
