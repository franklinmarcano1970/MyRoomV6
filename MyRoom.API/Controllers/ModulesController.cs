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
using MyRoom.Model;
using MyRoom.Data;
using MyRoom.Data.Repositories;
using MyRoom.Model.ViewModels;
using MyRoom.Data.Mappers;
using MyRoom.API.Filters;

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

                Module module = ModuleMapper.CreateModel(moduleViewModel);
                moduleRepo.CatalogStateUnchange(module);
                moduleRepo.Insert(module);

                //busco hotel con el catalogo seleccionado
                ActiveHotelCatalogRepository hotelCatalog = new ActiveHotelCatalogRepository(new MyRoomDbContext());
                int hotelId = hotelCatalog.GetByCatalogId(moduleViewModel.CatalogId);
                if (hotelId > 0)
                { 
                    //inserto categorias a hotel relacionado
                    ActiveHotelModuleRepository activeHotelModuleRepo = new ActiveHotelModuleRepository(new MyRoomDbContext());
                    List<ActiveHotelModule> hotelModules = new List<ActiveHotelModule>();
                    hotelModules.Add(new ActiveHotelModule() { IdModule = module.ModuleId, IdHotel = hotelId });
                    activeHotelModuleRepo.InsertActiveHotelModule(hotelModules, hotelId);
                }
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
        [HasModulesChildrenActionFilter]
        public async Task<IHttpActionResult> DeleteModules(int key)
        {
            await moduleRepo.DeleteAsync(key);
            return Ok(HttpStatusCode.NoContent);
        }

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
