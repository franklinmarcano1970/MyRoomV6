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

    public class RelCatalogueModulesController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/RelCatalogueModules
        [EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<RelCatalogueModule> GetRelCatalogueModules()
        {
            return db.RelCatalogueModule;
        }

        // GET: odata/RelCatalogueModules(5)
        [EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<RelCatalogueModule> GetRelCatalogueModule([FromODataUri] int key)
        {
            return SingleResult.Create(db.RelCatalogueModule.Where(relCatalogueModule => relCatalogueModule.IdCatalogue == key));
        }

        // PUT: odata/RelCatalogueModules(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<RelCatalogueModule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelCatalogueModule relCatalogueModule = await db.RelCatalogueModule.FindAsync(key);
            if (relCatalogueModule == null)
            {
                return NotFound();
            }

            patch.Put(relCatalogueModule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelCatalogueModuleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relCatalogueModule);
        }

        // POST: odata/RelCatalogueModules
        public async Task<IHttpActionResult> Post(RelCatalogueModule relCatalogueModule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RelCatalogueModule.Add(relCatalogueModule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RelCatalogueModuleExists(relCatalogueModule.IdCatalogue))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(relCatalogueModule);
        }

        // PATCH: odata/RelCatalogueModules(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<RelCatalogueModule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RelCatalogueModule relCatalogueModule = await db.RelCatalogueModule.FindAsync(key);
            if (relCatalogueModule == null)
            {
                return NotFound();
            }

            patch.Patch(relCatalogueModule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelCatalogueModuleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(relCatalogueModule);
        }

        // DELETE: odata/RelCatalogueModules(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            RelCatalogueModule relCatalogueModule = await db.RelCatalogueModule.FindAsync(key);
            if (relCatalogueModule == null)
            {
                return NotFound();
            }

            db.RelCatalogueModule.Remove(relCatalogueModule);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/RelCatalogueModules(5)/Catalogues
        [EnableQuery]
        public IQueryable<Catalog> GetCatalogues([FromODataUri] int key)
        {
            return db.RelCatalogueModule.Where(m => m.IdCatalogue == key).Select(m => m.Catalog);
        }

        // GET: odata/RelCatalogueModules(5)/Modules
        [EnableQuery]
        public IQueryable<Module> GetModules([FromODataUri] int key)
        {
            return db.RelCatalogueModule.Where(m => m.IdCatalogue == key).Select(m => m.Module);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RelCatalogueModuleExists(int key)
        {
            return db.RelCatalogueModule.Count(e => e.IdCatalogue == key) > 0;
        }
    }
}
