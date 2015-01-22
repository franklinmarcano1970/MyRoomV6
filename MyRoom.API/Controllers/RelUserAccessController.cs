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

    public class RelUserAccessController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/RelUserAccess
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<RelUserAccess> GetRelUserAccess()
        {
            return db.RelUserAccess;
        }

        // GET: odata/RelUserAccess(5)
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<RelUserAccess> GetRelUserAccess([FromODataUri] int key)
        {
            return SingleResult.Create(db.RelUserAccess.Where(relUserAccess => relUserAccess.Id == key));
        }

        // PUT: odata/RelUserAccess(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<RelUserAccess> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelUserAccess relUserAccess = await db.RelUserAccess.FindAsync(key);
            if (relUserAccess == null)
            {
                return NotFound();
            }

            patch.Put(relUserAccess);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelUserAccessExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relUserAccess);
        }

        // POST: odata/RelUserAccess
        public async Task<IHttpActionResult> Post(RelUserAccess relUserAccess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RelUserAccess.Add(relUserAccess);
            await db.SaveChangesAsync();

            return Created(relUserAccess);
        }

        // PATCH: odata/RelUserAccess(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<RelUserAccess> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelUserAccess relUserAccess = await db.RelUserAccess.FindAsync(key);
            if (relUserAccess == null)
            {
                return NotFound();
            }

            patch.Patch(relUserAccess);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelUserAccessExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relUserAccess);
        }

        // DELETE: odata/RelUserAccess(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            RelUserAccess relUserAccess = await db.RelUserAccess.FindAsync(key);
            if (relUserAccess == null)
            {
                return NotFound();
            }

            db.RelUserAccess.Remove(relUserAccess);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/RelUserAccess(5)/MenuAccess
        [EnableQuery]
        public SingleResult<MenuAccess> GetMenuAccess([FromODataUri] int key)
        {
            return SingleResult.Create(db.RelUserAccess.Where(m => m.Id == key).Select(m => m.MenuAccess));
        }

        // GET: odata/RelUserAccess(5)/User
        //[EnableQuery]
        //public SingleResult<User> GetUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.RelUserAccess.Where(m => m.Id == key).Select(m => m.User));
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RelUserAccessExists(int key)
        {
            return db.RelUserAccess.Count(e => e.Id == key) > 0;
        }
    }
}
