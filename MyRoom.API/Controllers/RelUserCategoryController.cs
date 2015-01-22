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

    public class RelUserCategoryController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/RelUserCategory
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<RelUserCategory> GetRelUserCategory()
        {
            return db.RelUserCategory;
        }

        // GET: odata/RelUserCategory(5)
        [EnableQuery( AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<RelUserCategory> GetRelUserCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.RelUserCategory.Where(relUserCategory => relUserCategory.Id == key));
        }

        // PUT: odata/RelUserCategory(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<RelUserCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelUserCategory relUserCategory = await db.RelUserCategory.FindAsync(key);
            if (relUserCategory == null)
            {
                return NotFound();
            }

            patch.Put(relUserCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelUserCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relUserCategory);
        }

        // POST: odata/RelUserCategory
        public async Task<IHttpActionResult> Post(RelUserCategory relUserCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RelUserCategory.Add(relUserCategory);
            await db.SaveChangesAsync();

            return Created(relUserCategory);
        }

        // PATCH: odata/RelUserCategory(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<RelUserCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelUserCategory relUserCategory = await db.RelUserCategory.FindAsync(key);
            if (relUserCategory == null)
            {
                return NotFound();
            }

            patch.Patch(relUserCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelUserCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relUserCategory);
        }

        // DELETE: odata/RelUserCategory(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            RelUserCategory relUserCategory = await db.RelUserCategory.FindAsync(key);
            if (relUserCategory == null)
            {
                return NotFound();
            }

            db.RelUserCategory.Remove(relUserCategory);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/RelUserCategory(5)/Categories
        //[EnableQuery]
        //public SingleResult<Category> GetCategories([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.RelUserCategory.Where(m => m.Id == key).Select(m => m.Categories));
        //}

        // GET: odata/RelUserCategory(5)/User
        //[EnableQuery]
        //public SingleResult<User> GetUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.RelUserCategory.Where(m => m.Id == key).Select(m => m.User));
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RelUserCategoryExists(int key)
        {
            return db.RelUserCategory.Count(e => e.Id == key) > 0;
        }
    }
}
