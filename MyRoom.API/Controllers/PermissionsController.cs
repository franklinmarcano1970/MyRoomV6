using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using MyRoom.Model;
using MyRoom.Data.Repositories;
using System.Linq;
using MyRoom.Data;
using System;
using System.Net;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.Description;
using System.Collections.Generic;

namespace MyRoom.API.Controllers
{

    [RoutePrefix("api/permissions")]
    public class PermissionsController : ApiController
    {

        //GET: api/permissions
        [HttpGet]
        public IHttpActionResult GetPermissions()
        {
            GenericRepository<MenuAccess> menuAccessRepository = new GenericRepository<MenuAccess>(new MyRoomDbContext());
            return Ok( menuAccessRepository.GetAll());
        }

        //GET: api/permissions/user/2          
        [Route("user/{userId}")]
        [HttpGet]
        public IHttpActionResult GetUserPermissions(string userId)
        {
            PermissionRepository permissionRepository = new PermissionRepository(new MyRoomDbContext());           
            return Ok( permissionRepository.GetById(userId));

        }

        [HttpPost]
        public IHttpActionResult PostPermissions([FromBody]List<Permission> permissions)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PermissionRepository permissionRepo = new PermissionRepository(new MyRoomDbContext());

            permissionRepo.InsertPermissions(permissions);

            return Ok("Permission Saved");

        }

        [Route("userhotel")]
        [HttpPost]
        public IHttpActionResult PostUserHotelPermissions([FromBody]List<UserHotelPermission> permissions)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserHotelPermissionRepository permissionRepo = new UserHotelPermissionRepository(new MyRoomDbContext());

            permissionRepo.InsertPermissions(permissions);

            return Ok("Permission Saved");

        }

        [Route("userhotel/{userId}")]
        [HttpGet]
        public IHttpActionResult GetUserHotelPermissions(string userId)
        {
            UserHotelPermissionRepository permissionRepository = new UserHotelPermissionRepository(new MyRoomDbContext());
            return Ok(permissionRepository.GetById(userId));

        }


    }
}
