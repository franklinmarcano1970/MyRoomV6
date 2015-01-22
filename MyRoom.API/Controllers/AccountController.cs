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

    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private AccountRepository _genericRepository = new AccountRepository(new MyRoomDbContext());

        public AccountController()
        {
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }


        // POST api/Account/ChangePassword
        //[Route("ChangePassword")]
        //public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
        //        model.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}

        //// POST api/Account/SetPassword
        //[Route("SetPassword")]
        //public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}


        // POST api/Account/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email, Name = model.Name, Surname = model.Surname, Active = model.Active};
            try
            {
                IdentityResult result = await _genericRepository.RegisterUser(user, model);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok("User Created");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         //PUT: api/account/1
        [ResponseType(typeof(ApplicationUser))]
        public async Task<IHttpActionResult> Put([FromBody]ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserManager<ApplicationUser> manager = _genericRepository.Manager;

            _genericRepository.Update(user);
            IdentityResult result = await _genericRepository.Manager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
         
            return Ok(user);
        }

        // Delete api/Account/1
        [Route("{key}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAccount(int key)
        {
            if (!ModelState.IsValid)
            {                
                return BadRequest(ModelState);
            }

            ApplicationUser user = _genericRepository.GetUserById(key);

            try
            {
                await _genericRepository.Manager.DeleteAsync(user);

                return Ok("User Deleted");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        [Route("Users")]
        public IQueryable<ApplicationUser> GetUsers()
        {
            return _genericRepository.GetUsers();
        }

        [Route("{key}")]
        [HttpGet]
        public IHttpActionResult GetAccount(int key)
        {
            //return _genericRepository.GetUsers();
            return Ok(_genericRepository.GetUserById(key));         
        }


        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

     

        #endregion
    }
}
