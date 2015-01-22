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

namespace MyRoom.API.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        CategoryRepository categoryRepo = new CategoryRepository(new MyRoomDbContext());
        // GET: api/Categories       
        public IHttpActionResult GetCategories()
        {
            return Ok(categoryRepo.GetAll());
        }

        [Route("{key}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCategories(int key)
        {
            try
            {
                Category category = await categoryRepo.GetByIdAsync(key);
                return Ok(category);
            }
            catch (Exception ex)
            {
                if (!CategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // PUT: api/Categories/
        public async Task<IHttpActionResult> PutCategories(Category category)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (category == null)
            {
                return NotFound();
            }

            try
            {
                await categoryRepo.EditAsync(category);
                return Ok("Category Updated");
            }
            catch (Exception ex)
            {
                if (!CategoryExists(category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }         
        }

        // POST: api/Categories
        public async Task<IHttpActionResult> PostCategories(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (category.Modules != null)
                { 
                    categoryRepo.Update(category);
                } 
                await categoryRepo.InsertAsync(category);

                return Ok(category);
            }
            catch (Exception ex)
            {
                if (!CategoryExists(category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }
        }

        // DELETE: api/Categories/5
        [Route("{key}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCategories(int key)
        {
            Category category = await categoryRepo.GetByIdAsync(key);
            if (category == null)
            {
                return NotFound();
            }

            try
            {
                await categoryRepo.DeleteAsync(category);
                return Ok("Category Deleted");

            }
            catch (Exception ex)
            {
                if (!CategoryExists(category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }
        }

        // GET: odata/Categories(5)/ActiveHotelCategory
        //[EnableQuery]
        //public IQueryable<ActiveHotelCategory> GetActiveHotelCategory([FromODataUri] int key)
        //{
        //    return db.Categories.Where(m => m.Id == key).SelectMany(m => m.ActiveHotelCategory);
        //}

        //// GET: odata/Categories(5)/RelCategoryProduct
        //[EnableQuery]
        //public IQueryable<RelCategoryProduct> GetRelCategoryProduct([FromODataUri] int key)
        //{
        //    return db.Categories.Where(m => m.Id == key).SelectMany(m => m.RelCategoryProduct);
        //}

        // GET: odata/Categories(5)/RelModuleCategory
        //[EnableQuery]
        //public IQueryable<RelModuleCategory> GetRelModuleCategory([FromODataUri] int key)
        //{
        //    return db.Categories.Where(m => m.Id == key).SelectMany(m => m.RelModuleCategory);
        //}

        // GET: odata/Categories(5)/RelUserCategory
        //[EnableQuery]
        //public IQueryable<RelUserCategory> GetRelUserCategory([FromODataUri] int key)
        //{
        //    return db.Categories.Where(m => m.Id == key).SelectMany(m => m.RelUserCategory);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                categoryRepo.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int key)
        {
            return categoryRepo.Context.Categories.Count(e => e.CategoryId == key) > 0;
        }
    }
}
