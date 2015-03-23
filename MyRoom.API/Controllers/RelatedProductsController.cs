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
    [Authorize]
    [RoutePrefix("api/relatedproducts")]
    public class RelatedProductsController : ApiController
    {
        RelatedProductRepository relatedProductRepository = new RelatedProductRepository(new MyRoomDbContext());

        [Route("{prodId}/{hotelId}")]
        [HttpGet]
        public IHttpActionResult GetRelatedProducts(int prodId, int hotelId)
        {
            return Ok(relatedProductRepository.GetActiveProductRelated(hotelId, prodId));
        }

        [Route("{hotelId}")]
        [HttpGet]
        public IHttpActionResult GetRelatedProductsByHotelId(int hotelId)
        {
            return Ok(relatedProductRepository.GetActiveProductRelated(hotelId));
        }

    }
}
