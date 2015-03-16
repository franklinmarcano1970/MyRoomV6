using MyRoom.Model;
using MyRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyRoom.Data.Repositories
{
    public class UserModuleRepository : GenericRepository<RelUserModule>
    {
        public MyRoomDbContext Context { get; private set; }
        public UserModuleRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }

        public void InsertUserModule(List<RelUserModule> items, string userId, bool deleteUserModules = false)
        {
            if (deleteUserModules)
            {
                this.DeleteUserModule(userId);
            }

            if (items.Count > 0)
            {
                items.ForEach(delegate(RelUserModule userModule)
                {
                    this.Insert(userModule);
                });
            }
        }

        public void DeleteUserModule(string userId)
        {
            List<RelUserModule> userModule = this.Context.RelUserModule.Where(c => c.IdUser == userId).ToList();
            try
            {
                this.DeleteCollection(userModule);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}