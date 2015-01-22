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
    public class RelModuleCategoryController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        //// GET: odata/RelModuleCategory
        //[EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.All)]
        //public IQueryable<RelModuleCategory> GetRelModuleCategory()
        //{
        //    return db.RelModuleCategory;
        //}

        //// GET: odata/RelModuleCategory(5)
        //[EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.All)]
        //public SingleResult<RelModuleCategory> GetRelModuleCategory([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.RelModuleCategory.Where(relModuleCategory => relModuleCategory.IdModule == key));
        //}

        //// PUT: odata/RelModuleCategory(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<RelModuleCategory> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    RelModuleCategory relModuleCategory = await db.RelModuleCategory.FindAsync(key);
        //    if (relModuleCategory == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(relModuleCategory);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RelModuleCategoryExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(relModuleCategory);
        //}

        //// POST: odata/RelModuleCategory
        //public async Task<IHttpActionResult> Post(RelModuleCategory relModuleCategory)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.RelModuleCategory.Add(relModuleCategory);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (RelModuleCategoryExists(relModuleCategory.IdModule))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Created(relModuleCategory);
        //}

        //// PATCH: odata/RelModuleCategory(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<RelModuleCategory> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    RelModuleCategory relModuleCategory = await db.RelModuleCategory.FindAsync(key);
        //    if (relModuleCategory == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(relModuleCategory);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RelModuleCategoryExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(relModuleCategory);
        //}

        //// DELETE: odata/RelModuleCategory(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    RelModuleCategory relModuleCategory = await db.RelModuleCategory.FindAsync(key);
        //    if (relModuleCategory == null)
        //    {
        //        return NotFound();
        //    }

        //    db.RelModuleCategory.Remove(relModuleCategory);
        //    await db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// GET: odata/RelModuleCategory(5)/Categories
        //[EnableQuery]
        //public IQueryable<Categories> GetCategories([FromODataUri] int key)
        //{
        //    return db.RelModuleCategory.Where(m => m.IdModule == key).Select(m => m.Categories);
        //}

        //// GET: odata/RelModuleCategory(5)/Modules
        //[EnableQuery]
        //public SingleResult<Module> GetModules([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.RelModuleCategory.Where(m => m.IdModule == key).Select(m => m.Module));
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool RelModuleCategoryExists(int key)
        //{
        //    return db.RelModuleCategory.Count(e => e.IdModule == key) > 0;
        //}
    }
}
