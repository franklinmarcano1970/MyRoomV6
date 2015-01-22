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

    public class RelUserCataloguesController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/RelUserCatalogues
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<RelUserCatalogue> GetRelUserCatalogues()
        {
            return db.RelUserCatalogue;
        }

        // GET: odata/RelUserCatalogues(5)
        [EnableQuery( AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<RelUserCatalogue> GetRelUserCatalogue([FromODataUri] int key)
        {
            return SingleResult.Create(db.RelUserCatalogue.Where(relUserCatalogue => relUserCatalogue.Id == key));
        }

        // PUT: odata/RelUserCatalogues(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<RelUserCatalogue> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelUserCatalogue relUserCatalogue = await db.RelUserCatalogue.FindAsync(key);
            if (relUserCatalogue == null)
            {
                return NotFound();
            }

            patch.Put(relUserCatalogue);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelUserCatalogueExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relUserCatalogue);
        }

        // POST: odata/RelUserCatalogues
        public async Task<IHttpActionResult> Post(RelUserCatalogue relUserCatalogue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RelUserCatalogue.Add(relUserCatalogue);
            await db.SaveChangesAsync();

            return Created(relUserCatalogue);
        }

        // PATCH: odata/RelUserCatalogues(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<RelUserCatalogue> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelUserCatalogue relUserCatalogue = await db.RelUserCatalogue.FindAsync(key);
            if (relUserCatalogue == null)
            {
                return NotFound();
            }

            patch.Patch(relUserCatalogue);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelUserCatalogueExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relUserCatalogue);
        }

        // DELETE: odata/RelUserCatalogues(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            RelUserCatalogue relUserCatalogue = await db.RelUserCatalogue.FindAsync(key);
            if (relUserCatalogue == null)
            {
                return NotFound();
            }

            db.RelUserCatalogue.Remove(relUserCatalogue);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/RelUserCatalogues(5)/Catalogues
        [EnableQuery]
        public SingleResult<Catalog> GetCatalogues([FromODataUri] int key)
        {
            return SingleResult.Create(db.RelUserCatalogue.Where(m => m.Id == key).Select(m => m.Catalog));
        }

        // GET: odata/RelUserCatalogues(5)/User
        //[EnableQuery]
        //public SingleResult<User> GetUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.RelUserCatalogue.Where(m => m.Id == key).Select(m => m.User));
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RelUserCatalogueExists(int key)
        {
            return db.RelUserCatalogue.Count(e => e.Id == key) > 0;
        }
    }
}
