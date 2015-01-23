using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using MyRoom.Model;
using MyRoom.Data;
using MyRoom.Data.Repositories;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace MyRoom.API.Controllers
{
    [RoutePrefix("api/catalogues")]
    public class CataloguesController : ApiController
    {
        CatalogRepository catalogRepository = new CatalogRepository(new MyRoomDbContext());

        // GET: api/Catalogues
        public IHttpActionResult GetCatalogues()
        {
            return Ok(catalogRepository.GetAll());
        }

        [Route("{key}")]
        [HttpGet]
        public string GetCatalogues(int key)
        {
            return catalogRepository.GetStructureComplete(key);
        }

        [Route("catalogbyid/{key}")]
        [HttpGet]
        public string GetCatalogueById(int key)
        {
            return catalogRepository.GetCatalogueById(key);

        }
        // GET: odata/Catalogues(5)

        //     //   [EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.All)]
        //        public SingleResult<Catalog> CatalogComplex()
        //        {
        //            return null;
        //            //return _genericRepository.GetStructureComplete(1);
        ////            return SingleResult.Create(_genericRepository.Context.Catalogues.Where(catalogues => catalogues.CatalogId == id));
        //        }

        // PUT: api/Catalogues
        public async Task<IHttpActionResult> PutCatalogues(Catalog catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                await catalogRepository.EditAsync(catalog);
            }
            catch (Exception ex)
            {
                if (!CataloguesExists(catalog.CatalogId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Catalog Updated");
        }

        //public async Task<IHttpActionResult> GetStructureCatalogues(Catalog catalog)
        //{
        //}

        // POST: api/Catalogues
        public async Task<IHttpActionResult> PostCatalogues(Catalog catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                catalogRepository.Insert(catalog);
                int catalogid = catalog.CatalogId;

                this.CreateStructureDirectories(catalogid);
                return Ok("The catalog has been inserted");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void CreateStructureDirectories(int catalogid)
        {

            string uploadFolder = ConfigurationManager.AppSettings["UploadImages"];

            try
            {

                uploadFolder = System.Web.HttpContext.Current.Server.MapPath(uploadFolder);

                if (!Directory.Exists(uploadFolder))
                { 
                    Directory.CreateDirectory(uploadFolder);
                }

             
                uploadFolder = string.Format("{0}\\{1}", uploadFolder, catalogid);
             
                string[] directories = new string[] {
                                uploadFolder  + "\\modules",
                                uploadFolder + "\\categories",
                                uploadFolder + "\\products",
                                uploadFolder + "\\moreinfo"            
                            };
                for (int n = 0; n < directories.Length; n++)
                {
                    Directory.CreateDirectory(directories[n]);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // DELETE: api/Catalogues/5
        [Route("{key}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCatalogues(int key)
        {
            Catalog catalog = await catalogRepository.GetByIdAsync(key);
            if (catalog == null)
            {
                return NotFound();
            }

            await catalogRepository.DeleteAsync(catalog);

            return StatusCode(HttpStatusCode.NoContent);
        }


        //public IQueryable<Catalog> GetStructureCatalogues([FromODataUri] int id)
        //{

        //    //_genericRepository.GetAll()
        //     return _genericRepository.GetStructure();

        //}
        // GET: odata/Catalogues(5)/ActiveHotelCatalogue
        //[EnableQuery]
        //public IQueryable<ActiveHotelCatalogue> GetActiveHotelCatalogue([FromODataUri] int key)
        //{
        //    return db.Catalogues.Where(m => m.Id == key).SelectMany(m => m.ActiveHotelCatalogue);
        //}

        // GET: odata/Catalogues(5)/RelCatalogueModule
        //[EnableQuery]
        //public IQueryable<RelCatalogueModule> GetRelCatalogueModule([FromODataUri] int key)
        //{

        //    return db.Catalogues.Where(m => m.Id == key).SelectMany(m => m.RelCatalogueModule);
        //}

        // GET: odata/Catalogues(5)/RelUserCatalogue
        //[EnableQuery]
        //public IQueryable<RelUserCatalogue> GetRelUserCatalogue([FromODataUri] int key)
        //{
        //    return db.Catalogues.Where(m => m.Id == key).SelectMany(m => m.RelUserCatalogue);
        //}

        // GET: odata/Catalogues(5)/Translations
        //[EnableQuery]
        //public SingleResult<Translation> GetTranslations([FromODataUri] int key)
        //{
        //    //throw new NotImplementedException();
        //    return SingleResult.Create(db.Catalogues.Where(m => m.Id == key).Select(m => m.Translation));
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                catalogRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CataloguesExists(int key)
        {
            return catalogRepository.Context.Catalogues.Count(e => e.CatalogId == key) > 0;
        }
    }
}
