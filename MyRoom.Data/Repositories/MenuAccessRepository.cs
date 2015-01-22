using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyRoom.Data.Repositories
{
    public class MenuAccessRepository : GenericRepository<MenuAccess>
    {
        public MenuAccessRepository(MyRoomDbContext context)
            : base(context)
        {
             this.Context = context;
        }

        public MyRoomDbContext Context { get; private set; }
    }
}