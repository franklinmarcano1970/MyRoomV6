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
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using MyRoom.Model;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<RelUserHotel>("RelUserHotels");
    builder.EntitySet<Hotels>("Hotels"); 
    builder.EntitySet<User>("User"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RelUserHotelsController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/RelUserHotels
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<RelUserHotel> GetRelUserHotels()
        {
            return db.RelUserHotel;
        }

        // GET: odata/RelUserHotels(5)
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<RelUserHotel> GetRelUserHotel([FromODataUri] int key)
        {
            return SingleResult.Create(db.RelUserHotel.Where(relUserHotel => relUserHotel.Id == key));
        }

        // PUT: odata/RelUserHotels(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<RelUserHotel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelUserHotel relUserHotel = await db.RelUserHotel.FindAsync(key);
            if (relUserHotel == null)
            {
                return NotFound();
            }

            patch.Put(relUserHotel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelUserHotelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relUserHotel);
        }

        // POST: odata/RelUserHotels
        public async Task<IHttpActionResult> Post(RelUserHotel relUserHotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RelUserHotel.Add(relUserHotel);
            await db.SaveChangesAsync();

            return Created(relUserHotel);
        }

        // PATCH: odata/RelUserHotels(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<RelUserHotel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelUserHotel relUserHotel = await db.RelUserHotel.FindAsync(key);
            if (relUserHotel == null)
            {
                return NotFound();
            }

            patch.Patch(relUserHotel);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelUserHotelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relUserHotel);
        }

        // DELETE: odata/RelUserHotels(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            RelUserHotel relUserHotel = await db.RelUserHotel.FindAsync(key);
            if (relUserHotel == null)
            {
                return NotFound();
            }

            db.RelUserHotel.Remove(relUserHotel);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/RelUserHotels(5)/Hotels
        [EnableQuery]
        public SingleResult<Hotel> GetHotels([FromODataUri] int key)
        {
            return SingleResult.Create(db.RelUserHotel.Where(m => m.Id == key).Select(m => m.Hotel));
        }

        // GET: odata/RelUserHotels(5)/User
        //[EnableQuery]
        //public SingleResult<User> GetUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.RelUserHotel.Where(m => m.Id == key).Select(m => m.User));
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RelUserHotelExists(int key)
        {
            return db.RelUserHotel.Count(e => e.Id == key) > 0;
        }
    }
}
