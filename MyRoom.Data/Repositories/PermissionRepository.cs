using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyRoom.Data.Repositories
{
    public class PermissionRepository : GenericRepository<Permission>
    {
        public MyRoomDbContext Context { get; private set; }
        public PermissionRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }

        public void InsertPermissions(List<Permission> permissions)
        {
            this.DeletePermissionsByUser(permissions[0].IdUser);
            if (permissions[0].IdPermission != 0)
            {
                permissions.ForEach(delegate(Permission permission)
                {
                    this.Insert(permission);

                });
            }
        }

        public void DeletePermissionsByUser(string userId)
        {
            List<Permission> permissions = this.Context.Permissions.Where(c => c.IdUser == userId).ToList();
            try
            {
                this.DeleteCollection(permissions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Permission> GetById(string id)
        {
            return this.Context.Permissions.Where(c => c.IdUser == id);
        }
    }
}