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
    public class UserCatalogRepository : GenericRepository<RelUserCatalogue>
    {
        public MyRoomDbContext Context { get; private set; }
        public UserCatalogRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }

        public void InsertUserCatalog(UserCatalogViewModel userCatalog, bool deleteUserCatalog = false)
        {
            if (deleteUserCatalog)
            {
                this.DeleteUserCatalog(userCatalog.UserId);
            }

            this.Insert(new RelUserCatalogue()
            {
                IdUser = userCatalog.UserId,
                IdCatalogue = userCatalog.CatalogId,
                ReadOnly = true,
                ReadWrite = false
            });
           
        }

        public void DeleteUserCatalog(string userId)
        {
            List<RelUserCatalogue> userCatalog = this.Context.RelUserCatalogue.Where(c => c.IdUser == userId).ToList();
            try
            {
                this.DeleteCollection(userCatalog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}