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

    public class ActiveHotelCategoriesController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/ActiveHotelCategories
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<ActiveHotelCategory> GetActiveHotelCategories()
        {
            return db.ActiveHotelCategory;
        }

        // GET: odata/ActiveHotelCategories(5)
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<ActiveHotelCategory> GetActiveHotelCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.ActiveHotelCategory.Where(activeHotelCategory => activeHotelCategory.IdHotel == key));
        }

        // PUT: odata/ActiveHotelCategories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<ActiveHotelCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActiveHotelCategory activeHotelCategory = await db.ActiveHotelCategory.FindAsync(key);
            if (activeHotelCategory == null)
            {
                return NotFound();
            }

            patch.Put(activeHotelCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveHotelCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(activeHotelCategory);
        }

        // POST: odata/ActiveHotelCategories
        public async Task<IHttpActionResult> Post(ActiveHotelCategory activeHotelCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActiveHotelCategory.Add(activeHotelCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ActiveHotelCategoryExists(activeHotelCategory.IdHotel))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(activeHotelCategory);
        }

        // PATCH: odata/ActiveHotelCategories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ActiveHotelCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActiveHotelCategory activeHotelCategory = await db.ActiveHotelCategory.FindAsync(key);
            if (activeHotelCategory == null)
            {
                return NotFound();
            }

            patch.Patch(activeHotelCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveHotelCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(activeHotelCategory);
        }

        // DELETE: odata/ActiveHotelCategories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            ActiveHotelCategory activeHotelCategory = await db.ActiveHotelCategory.FindAsync(key);
            if (activeHotelCategory == null)
            {
                return NotFound();
            }

            db.ActiveHotelCategory.Remove(activeHotelCategory);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ActiveHotelCategories(5)/Categories
        //[EnableQuery]
        //public SingleResult<Category> GetCategories([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.ActiveHotelCategory.Where(m => m.IdHotel == key).Select(m => m.Categories));
        //}

        // GET: odata/ActiveHotelCategories(5)/Hotels
        [EnableQuery]
        public SingleResult<Hotel> GetHotels([FromODataUri] int key)
        {
            return SingleResult.Create(db.ActiveHotelCategory.Where(m => m.IdHotel == key).Select(m => m.Hotel));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActiveHotelCategoryExists(int key)
        {
            return db.ActiveHotelCategory.Count(e => e.IdHotel == key) > 0;
        }
    }
}
