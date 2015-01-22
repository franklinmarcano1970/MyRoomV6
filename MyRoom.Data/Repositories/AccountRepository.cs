using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRoom.Data.Repositories
{
    public class AccountRepository : GenericRepository<ApplicationUser>
    {
        private UserManager<ApplicationUser> userManager;


        public AccountRepository(MyRoomDbContext context)
            : base(context)
        {            
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            this.Manager = userManager;
            this.Context = context;
        }


        public async Task<IdentityResult> RegisterUser(ApplicationUser user, RegisterBindingModel model)
        {

            try
            {
                var result = await userManager.CreateAsync(user, model.Password);

                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
                string roleName = "Customers";
                IdentityResult roleResult;

                // Check to see if Role Exists, if not create it
              //  roleResult = RoleManager.Create(new IdentityRole(roleName));

               // userManager.AddToRole(user.Id, roleName);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        public async Task<IdentityResult> UpdateUser(ApplicationUser user)
        {
            var result = await userManager.UpdateAsync(user);
            return result;
        }


        //public async Task<IdentityResult> Delete(ApplicationUser user)
        //{
        //    var result = await userManager.DeleteAsync(user);
        //    return result;
        //}

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await userManager.FindAsync(userName, password);
            return user;
        }

        public IQueryable<ApplicationUser> GetUsers()
        {
            return userManager.Users;
        }

        public ApplicationUser GetUserById(int id)
        {
            return userManager.Users.Where(u => u.ApplicationUserId == id).First();
        }


        public Client FindClient(string clientId)
        {
            var client = this.Context.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = this.Context.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            this.Context.RefreshTokens.Add(token);

            return await this.Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await this.Context.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                this.Context.RefreshTokens.Remove(refreshToken);
                return await this.Context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            this.Context.RefreshTokens.Remove(refreshToken);
            return await this.Context.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await this.Context.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return this.Context.RefreshTokens.ToList();
        }

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await userManager.FindAsync(loginInfo);

            return user;
        }

        public UserManager<ApplicationUser> Manager { get; set; }

        public MyRoomDbContext Context { get; private set; }
    }
}