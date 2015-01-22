using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyRoom.Data.Repositories
{
    public class UserHotelPermissionRepository : GenericRepository<UserHotelPermission>
    {
        public MyRoomDbContext Context { get; private set; }
        public UserHotelPermissionRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }

        public void InsertPermissions(List<UserHotelPermission> permissions)
        {
            this.DeletePermissionsByUser(permissions[0].IdUser);
            if (permissions[0].IdHotel != 0)
            {
                permissions.ForEach(delegate(UserHotelPermission permission)
                {
                    this.Insert(permission);

                });
            }
        }

        public void DeletePermissionsByUser(string userId)
        {
            List<UserHotelPermission> permissions = this.Context.UserHotelPermissions.Where(c => c.IdUser == userId).ToList();
            try
            {
                this.DeleteCollection(permissions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<UserHotelPermission> GetById(string id)
        {
            return this.Context.UserHotelPermissions.Where(c => c.IdUser == id);
        }
    }
}