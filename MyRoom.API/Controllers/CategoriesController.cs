using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using MyRoom.Model;
using MyRoom.Data;
using MyRoom.Data.Repositories;
using MyRoom.Model.ViewModels;
using MyRoom.Data.Mappers;
using MyRoom.ViewModels;

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

        // POST: api/Categories/products

        //[Route("products")]
        //[HttpPost]
        //public IHttpActionResult PostCategoriesWithProducts(ICollection<Category> categories)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        CategoryProductRepository categpryProductRepo = new CategoryProductRepository(new MyRoomDbContext());
              
        //        categpryProductRepo.InsertCategoryProduct(categories.ToList());

        //        return Ok("Category Product Inserted");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        // POST: api/Categories
        public IHttpActionResult PostCategories(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //if (category.Modules != null)
                //{ 
                //    categoryRepo.Update(category);
                //} 
                Category category = CategoryMapper.CreateModel(categoryViewModel);
                categoryRepo.ModuleStateUnchange(category);
                 
                if (!category.IsFirst)
                {
                    category.Modules = null;
                }

                categoryRepo.Insert(category);

                //busco hotel con el catalogo seleccionado
                ActiveHotelCatalogRepository hotelCatalog = new ActiveHotelCatalogRepository(new MyRoomDbContext());
                int hotelId = hotelCatalog.GetByCatalogId(categoryViewModel.CatalogId);
                //inserto categorias a hotel relacionado
                ActiveHotelCategoryRepository activeHotelCategoryRepo = new ActiveHotelCategoryRepository(new MyRoomDbContext());
                List<ActiveHotelCategory> hotelscategories = new List<ActiveHotelCategory>();
                hotelscategories.Add(new ActiveHotelCategory() { IdCategory = category.CategoryId, IdHotel = hotelId });
                activeHotelCategoryRepo.InsertActiveHotelCategory(hotelscategories, hotelId);

                return Ok(category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: api/categories/assignproducts
        [Route("assignproducts")]
        [HttpPost]
        public IHttpActionResult  PostCategoryAssignProducts(CategoryProductViewModel categoryAssignProductViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //    catalogRepository.Insert(catalog);
                //    int catalogid = catalog.CatalogId;
                CategoryProductRepository categoryProdRepo = new CategoryProductRepository(new MyRoomDbContext());
                List<CategoryProduct> categoryProds = CategoryProductMapper.CreateModel(categoryAssignProductViewModel);
                if (categoryProds.Count>0)
                     categoryProdRepo.InsertCategoryProduct(categoryProds);
                else
                    categoryProdRepo.DeleteCategoryProduct(categoryAssignProductViewModel.CategoryId);
                 
                //    this.CreateStructureDirectories(catalogid);
                return Ok("The products has been assigned to category");

            }
            catch (Exception ex)
            {
                throw ex;
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
