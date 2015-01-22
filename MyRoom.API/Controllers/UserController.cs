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
    public class UserController : ODataController
    {
        private MyRoomDbContext db = new MyRoomDbContext();

        // GET: odata/User
        [EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<User> GetUser()
        {
            return db.Users;
        }

        // GET: odata/User(5)
        [EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.Users.Where(user => user.Id == key));
        }

        // PUT: odata/User(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<User> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await db.Users.FindAsync(key);
            if (user == null)
            {
                return NotFound();
            }

            patch.Put(user);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(user);
        }

        // POST: odata/User
        public async Task<IHttpActionResult> Post(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return Created(user);
        }

        // PATCH: odata/User(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<User> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await db.Users.FindAsync(key);
            if (user == null)
            {
                return NotFound();
            }

            patch.Patch(user);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(user);
        }

        // DELETE: odata/User(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            User user = await db.Users.FindAsync(key);
            if (user == null)
            {
                return NotFound();
            }

            db.User.Remove(user);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/User(5)/RelUserAccess
        [EnableQuery]
        public IQueryable<RelUserAccess> GetRelUserAccess([FromODataUri] int key)
        {
            return db.Users.Where(m => m.Id == key).SelectMany(m => m.RelUserAccess);
        }

        // GET: odata/User(5)/RelUserCatalogue
        [EnableQuery]
        public IQueryable<RelUserCatalogue> GetRelUserCatalogue([FromODataUri] int key)
        {
            return db.User.Where(m => m.Id == key).SelectMany(m => m.RelUserCatalogue);
        }

        // GET: odata/User(5)/RelUserCategory
        [EnableQuery]
        public IQueryable<RelUserCategory> GetRelUserCategory([FromODataUri] int key)
        {
            return db.User.Where(m => m.Id == key).SelectMany(m => m.RelUserCategory);
        }

        // GET: odata/User(5)/RelUserHotel
        [EnableQuery]
        public IQueryable<RelUserHotel> GetRelUserHotel([FromODataUri] int key)
        {
            return db.User.Where(m => m.Id == key).SelectMany(m => m.RelUserHotel);
        }

        // GET: odata/User(5)/RelUserModule
        [EnableQuery]
        public IQueryable<RelUserModule> GetRelUserModule([FromODataUri] int key)
        {
            return db.User.Where(m => m.Id == key).SelectMany(m => m.RelUserModule);
        }

        // GET: odata/User(5)/RelUserProduct
        [EnableQuery]
        public IQueryable<RelUserProduct> GetRelUserProduct([FromODataUri] int key)
        {
            return db.User.Where(m => m.Id == key).SelectMany(m => m.RelUserProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int key)
        {
            return db.User.Count(e => e.Id == key) > 0;
        }
    }
}
