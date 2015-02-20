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
using MyRoom.Data.Repositories;
using MyRoom.Model.ViewModels;
using System.Configuration;

namespace MyRoom.API.Controllers
{

    [RoutePrefix("api/modules")]
    public class ModulesController : ApiController
    {
        ModuleRepository moduleRepo = new ModuleRepository(new MyRoomDbContext());

        // GET: api/Modules
        public IHttpActionResult GetModules()
        {
            return Ok(moduleRepo.GetAll());
        }

        [Route("{key}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetModules(int key)
        {
            try
            {
                Module module = await moduleRepo.GetByIdAsync(key);
                return Ok(module);
            }
            catch (Exception ex)
            {
                if (!ModuleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        
        }

        public async Task<IHttpActionResult> PutModules(Module module)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                await moduleRepo.EditAsync(module);
                return Ok("Module Updated");
            }
            catch (Exception ex)
            {
                if (!ModuleExists(module.ModuleId))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }
        }

        // POST: api/modules
        public IHttpActionResult Post(ModuleViewModel moduleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Module module = new Module() {
                    Active = moduleViewModel.ModuleActive,
                    Name = moduleViewModel.Name,
                     Orden = moduleViewModel.Orden,
                     Image =  moduleViewModel.Image,
                     Pending = moduleViewModel.Pending,
                     Prefix = moduleViewModel.Prefix,
                    Comment = moduleViewModel.Comment
                };

                module.Translation = new Translation() {
                    Spanish = moduleViewModel.Spanish,
                    English = moduleViewModel.English,
                    French = moduleViewModel.French,
                    German = moduleViewModel.German,
                    Language5 = moduleViewModel.Language5,
                    Language6 = moduleViewModel.Language6,
                    Language7 = moduleViewModel.Language7,
                    Language8 = moduleViewModel.Language8,
                    Active = moduleViewModel.TranslationActive,
                };
                module.Catalogues = new List<Catalog>();

                module.Catalogues.Add(new Catalog()
                {
                    CatalogId = moduleViewModel.CatalogId,
                    Name = moduleViewModel.CatalogName
                });

                moduleRepo.CatalogStateUnchange(module);
                moduleRepo.Insert(module);


                //busco hotel con el catalogo seleccionado
                ActiveHotelCatalogRepository hotelCatalog = new ActiveHotelCatalogRepository(new MyRoomDbContext());
                int hotelId = hotelCatalog.GetByCatalogId(moduleViewModel.CatalogId);
                //inserto categorias a hotel relacionado
                ActiveHotelModuleRepository activeHotelModuleRepo = new ActiveHotelModuleRepository(new MyRoomDbContext());
                List<ActiveHotelModule> hotelModules = new List<ActiveHotelModule>();
                hotelModules.Add(new ActiveHotelModule() { IdModule = module.ModuleId, IdHotel = hotelId });
                activeHotelModuleRepo.InsertActiveHotelModule(hotelModules, hotelId);

                return Ok(module.ModuleId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        
        // DELETE: api/modules/5
        [Route("{key}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteModules(int key)
        {
            Module module = await moduleRepo.GetByIdAsync(key);
            if (module == null)
            {
                return NotFound();
            }
            try
            {
                await moduleRepo.DeleteAsync(module);
            }
            catch (Exception ex)
            {
                if (!ModuleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }
           
            return Ok("Module Deleted");
        }

        // GET: odata/Modules(5)/ActiveHotelModule
        //[EnableQuery]
        //public IQueryable<ActiveHotelModule> GetActiveHotelModule([FromODataUri] int key)
        //{
        //    return db.Modules.Where(m => m.Id == key).SelectMany(m => m.ActiveHotelModule);
        //}

        // GET: odata/Modules(5)/RelCatalogueModule
       // [EnableQuery]
        //public IQueryable<RelCatalogueModule> GetRelCatalogueModule([FromODataUri] int key)
        //{
        //    return db.Modules.Where(m => m.Id == key).SelectMany(m => m.RelCatalogueModule);
        //}

        // GET: odata/Modules(5)/RelModuleCategory
        //[EnableQuery]
        //public IQueryable<RelModuleCategory> GetRelModuleCategory([FromODataUri] int key)
        //{
        //    return db.Modules.Where(m => m.Id == key).SelectMany(m => m.RelModuleCategory);
        //}

        //// GET: odata/Modules(5)/RelUserModule
        //[EnableQuery]
        //public IQueryable<RelUserModule> GetRelUserModule([FromODataUri] int key)
        //{
        //    return db.Modules.Where(m => m.Id == key).SelectMany(m => m.RelUserModule);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                moduleRepo.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModuleExists(int key)
        {
            return moduleRepo.Context.Modules.Count(e => e.ModuleId == key) > 0;
        }
    }
}
