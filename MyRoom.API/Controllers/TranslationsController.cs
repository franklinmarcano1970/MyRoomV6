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
    public class TranslationsController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/Translations
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Translation> GetTranslations()
        {
            return db.Translations;
        }

        // GET: odata/Translations(5)
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<Translation> GetTranslations([FromODataUri] int key)
        {
            return SingleResult.Create(db.Translations.Where(translations => translations.Id == key));
        }

        // PUT: odata/Translations(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Translation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Translation translation = await db.Translations.FindAsync(key);
            if (translation == null)
            {
                return NotFound();
            }

            patch.Put(translation);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranslationsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(translation);
        }

        // POST: odata/Translations
        public async Task<IHttpActionResult> Post(Translation translations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Translations.Add(translations);
            await db.SaveChangesAsync();

            return Created(translations);
        }

        // PATCH: odata/Translations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Translation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Translation translation = await db.Translations.FindAsync(key);
            if (translation == null)
            {
                return NotFound();
            }

            patch.Patch(translation);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranslationsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(translation);
        }

        // DELETE: odata/Translations(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Translation translation = await db.Translations.FindAsync(key);
            if (translation == null)
            {
                return NotFound();
            }

            db.Translations.Remove(translation);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Translations(5)/Catalogues
        [EnableQuery]
        public IQueryable<Catalog> GetCatalogues([FromODataUri] int key)
        {
            return db.Translations.Where(m => m.Id == key).SelectMany(m => m.Catalogues);
        }

        // GET: odata/Translations(5)/Categories
        [EnableQuery]
        public IQueryable<Category> GetCategories([FromODataUri] int key)
        {
            return db.Translations.Where(m => m.Id == key).SelectMany(m => m.Categories);
        }

        // GET: odata/Translations(5)/Hotels
        [EnableQuery]
        public IQueryable<Hotel> GetHotels([FromODataUri] int key)
        {
            return db.Translations.Where(m => m.Id == key).SelectMany(m => m.Hotels);
        }

        // GET: odata/Translations(5)/Modules
        [EnableQuery]
        public IQueryable<Module> GetModules([FromODataUri] int key)
        {
            return db.Translations.Where(m => m.Id == key).SelectMany(m => m.Modules);
        }

        // GET: odata/Translations(5)/Products
        [EnableQuery]
        public IQueryable<Product> GetProducts([FromODataUri] int key)
        {
            return db.Translations.Where(m => m.Id == key).SelectMany(m => m.Products);
        }

        //// GET: odata/Translations(5)/Products1
        //[EnableQuery]
        //public IQueryable<Product> GetProducts1([FromODataUri] int key)
        //{
        //    return db.Translations.Where(m => m.Id == key).SelectMany(m => m.Products1);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TranslationsExists(int key)
        {
            return db.Translations.Count(e => e.Id == key) > 0;
        }
    }
}
