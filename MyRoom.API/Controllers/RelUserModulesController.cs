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

    public class RelUserModulesController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/RelUserModules
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<RelUserModule> GetRelUserModules()
        {
            return db.RelUserModule;
        }

        // GET: odata/RelUserModules(5)
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<RelUserModule> GetRelUserModule([FromODataUri] int key)
        {
            return SingleResult.Create(db.RelUserModule.Where(relUserModule => relUserModule.Id == key));
        }

        // PUT: odata/RelUserModules(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<RelUserModule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelUserModule relUserModule = await db.RelUserModule.FindAsync(key);
            if (relUserModule == null)
            {
                return NotFound();
            }

            patch.Put(relUserModule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelUserModuleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relUserModule);
        }

        // POST: odata/RelUserModules
        public async Task<IHttpActionResult> Post(RelUserModule relUserModule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RelUserModule.Add(relUserModule);
            await db.SaveChangesAsync();

            return Created(relUserModule);
        }

        // PATCH: odata/RelUserModules(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<RelUserModule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelUserModule relUserModule = await db.RelUserModule.FindAsync(key);
            if (relUserModule == null)
            {
                return NotFound();
            }

            patch.Patch(relUserModule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelUserModuleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relUserModule);
        }

        // DELETE: odata/RelUserModules(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            RelUserModule relUserModule = await db.RelUserModule.FindAsync(key);
            if (relUserModule == null)
            {
                return NotFound();
            }

            db.RelUserModule.Remove(relUserModule);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/RelUserModules(5)/Modules
        [EnableQuery]
        public SingleResult<Module> GetModules([FromODataUri] int key)
        {
            return SingleResult.Create(db.RelUserModule.Where(m => m.Id == key).Select(m => m.Module));
        }

        // GET: odata/RelUserModules(5)/User
       // [EnableQuery]
        //public SingleResult<User> GetUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.RelUserModule.Where(m => m.Id == key).Select(m => m.User));
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RelUserModuleExists(int key)
        {
            return db.RelUserModule.Count(e => e.Id == key) > 0;
        }
    }
}
