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

    public class ActiveHotelModuleController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/ActiveHotelModule
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<ActiveHotelModule> GetActiveHotelModule()
        {
            return db.ActiveHotelModule;
        }

        // GET: odata/ActiveHotelModule(5)
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<ActiveHotelModule> GetActiveHotelModule([FromODataUri] int key)
        {
            return SingleResult.Create(db.ActiveHotelModule.Where(activeHotelModule => activeHotelModule.IdHotel == key));
        }

        // PUT: odata/ActiveHotelModule(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<ActiveHotelModule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActiveHotelModule activeHotelModule = await db.ActiveHotelModule.FindAsync(key);
            if (activeHotelModule == null)
            {
                return NotFound();
            }

            patch.Put(activeHotelModule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveHotelModuleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(activeHotelModule);
        }

        // POST: odata/ActiveHotelModule
        public async Task<IHttpActionResult> Post(ActiveHotelModule activeHotelModule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActiveHotelModule.Add(activeHotelModule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ActiveHotelModuleExists(activeHotelModule.IdHotel))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(activeHotelModule);
        }

        // PATCH: odata/ActiveHotelModule(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ActiveHotelModule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActiveHotelModule activeHotelModule = await db.ActiveHotelModule.FindAsync(key);
            if (activeHotelModule == null)
            {
                return NotFound();
            }

            patch.Patch(activeHotelModule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveHotelModuleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(activeHotelModule);
        }

        // DELETE: odata/ActiveHotelModule(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            ActiveHotelModule activeHotelModule = await db.ActiveHotelModule.FindAsync(key);
            if (activeHotelModule == null)
            {
                return NotFound();
            }

            db.ActiveHotelModule.Remove(activeHotelModule);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ActiveHotelModule(5)/Hotels
        [EnableQuery]
        public SingleResult<Hotel> GetHotels([FromODataUri] int key)
        {
            return SingleResult.Create(db.ActiveHotelModule.Where(m => m.IdHotel == key).Select(m => m.Hotel));
        }

        // GET: odata/ActiveHotelModule(5)/Modules
        [EnableQuery]
        public SingleResult<Module> GetModules([FromODataUri] int key)
        {
            return SingleResult.Create(db.ActiveHotelModule.Where(m => m.IdHotel == key).Select(m => m.Module));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActiveHotelModuleExists(int key)
        {
            return db.ActiveHotelModule.Count(e => e.IdHotel == key) > 0;
        }
    }
}
