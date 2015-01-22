﻿using System;
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
using MyRoom.API.Filters;
using System.Web.Http.OData.Query;
using MyRoom.Data;
using MyRoom.Data.Repositories;

namespace MyRoom.API.Controllers
{
    //[CustomAuthorize]
    //[MyBasicAuthenticationFilter]
    //[BasicAuthenticationFilter]
    [Authorize]
    [RoutePrefix("api/hotels")]
    public class HotelsController : ApiController
    {
        HotelRepository hotelRepository = new HotelRepository(new MyRoomDbContext());


        // GET: odata/Hotels        
           public IHttpActionResult GetHotels()
        {
            return Ok( hotelRepository.GetAll());
        }

        // GET: odata/Hotels(5)

        [Route("{key}")]
        [HttpGet]
        public IHttpActionResult GetHotels(int key)
        {
            // var hotel = hotelRepository.Context.Hotels.Where(hotels => hotels.Id == key).Include(hotels => hotels.Translation).ToList();
            return Ok(hotelRepository.GetHotelsById(key));
            //return hotelRepository.Context.Hotels.Where(hotels => hotels.Id == key);//.Include(hotels => hotels.Translation).ToList();//.Select(hotels => hotels.Translation);//.Select(hotels => hotels.Translation));
        }

        // PUT: api/Hotels
        public async Task<IHttpActionResult> PutHotels(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            try
            {
                await hotelRepository.EditAsync(hotel);
            }
            catch (Exception ex)
            {
                if (!HotelsExists(hotel.HotelId))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }

            return Ok(hotel);
        }

        // POST: odata/Hotels
        public async Task<IHttpActionResult> PostHotels(Hotel hotels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await hotelRepository.InsertAsync(hotels);

            return Ok(hotels);
        }

        // PATCH: odata/Hotels(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Hotel> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Hotel hotels = await db.Hotels.FindAsync(key);
        //    if (hotels == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(hotels);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!HotelsExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(hotels);
        //}

        // DELETE: api/hotels/5
        [Route("{key}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteHotels(int key)
        {
            Hotel hotels = await hotelRepository.GetByIdAsync(key);
            if (hotels == null)
            {
                return NotFound();
            }

            await hotelRepository.DeleteAsync(hotels);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Hotels(5)/ActiveHotelCatalogue
        //[EnableQuery]
        //public IQueryable<ActiveHotelCatalogue> GetActiveHotelCatalogue([FromODataUri] int key)
        //{
        //    return db.Hotels.Where(m => m.Id == key).SelectMany(m => m.ActiveHotelCatalogue);
        //}

        // GET: odata/Hotels(5)/ActiveHotelCategory
        //[EnableQuery]
        //public IQueryable<ActiveHotelCategory> GetActiveHotelCategory([FromODataUri] int key)
        //{
        //    return db.Hotels.Where(m => m.Id == key).SelectMany(m => m.ActiveHotelCategory);
        //}

        // GET: odata/Hotels(5)/ActiveHotelModule
        //[EnableQuery]
        //public IQueryable<ActiveHotelModule> GetActiveHotelModule([FromODataUri] int key)
        //{
        //    return db.Hotels.Where(m => m.Id == key).SelectMany(m => m.ActiveHotelModule);
        //}

        // GET: odata/Hotels(5)/ActiveHotelProduct
        //[EnableQuery]
        //public IQueryable<ActiveHotelProduct> GetActiveHotelProduct([FromODataUri] int key)
        //{
        //    return db.Hotels.Where(m => m.Id == key).SelectMany(m => m.ActiveHotelProduct);
        //}

        // GET: odata/Hotels(5)/RelUserHotel
        //[EnableQuery]
        //public IQueryable<RelUserHotel> GetRelUserHotel([FromODataUri] int key)
        //{
        //    return db.Hotels.Where(m => m.Id == key).SelectMany(m => m.RelUserHotel);
        //}

        // GET: odata/Hotels(5)/Translations
        //[EnableQuery]
        //public SingleResult<Translation> GetTranslations([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.Hotels.Where(m => m.Id == key).Select(m => m.Translation));
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                hotelRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HotelsExists(int key)
        {
            return hotelRepository.Context.Hotels.Count(e => e.HotelId == key) > 0;
        }
    }
}
