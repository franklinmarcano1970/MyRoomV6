using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using MyRoom.Model;
using MyRoom.Data;
using MyRoom.Data.Repositories;
using MyRoom.ViewModels;
using MyRoom.Data.Mappers;

namespace MyRoom.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/rooms")]
    public class RoomsController : ApiController
    {
        RoomRepository roomRepository = new RoomRepository(new MyRoomDbContext());

        // GET: api/departments
        public IHttpActionResult GetRoom()
        {
            return Ok(roomRepository.GetAll());
        }

        // GET: api/rooms/5
        [Route("{key}")]
        [HttpGet]
        public IHttpActionResult GetRoom(int key)
        {
            return Ok(roomRepository.GetById(key));
        }

        // PUT: api/rooms
        public async Task<IHttpActionResult> PutRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            try
            {
                await roomRepository.EditAsync(room);
            }
            catch (Exception ex)
            {
                if (!RoomExists(room.RoomId))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }

            return Ok(room);
        }

        // POST: api/hotels/
        public async Task<IHttpActionResult> PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await roomRepository.InsertAsync(room);

            return Ok("The room has been inserted");
        }
        
      
        // DELETE: api/rooms/5
        [Route("{key}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRoom(int key)
        {
            Room room = await roomRepository.GetByIdAsync(key);
            if (room == null)
            {
                return NotFound();
            }

            await roomRepository.DeleteAsync(room);

            return StatusCode(HttpStatusCode.NoContent);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                roomRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int key)
        {
            return roomRepository.Context.Rooms.Count(e => e.RoomId == key) > 0;
        }
    }
}
