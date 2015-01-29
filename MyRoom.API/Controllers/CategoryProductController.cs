using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MyRoom.Data;
using MyRoom.Data.Repositories;

namespace MyRoom.API.Controllers
{
    [RoutePrefix("api/categoryproduct")]
    public class CategoryProductController : ApiController
    {
        // GET: api/CategoryProduct/5
        [Route("{key}")]
        [HttpGet]
        public IHttpActionResult GetCategoryProduct(int key)
        {
            CategoryProductRepository catProd = new CategoryProductRepository(new MyRoomDbContext());
            return Json(catProd.GetCategoryById(key));
        }
    }
}
