using MyRoom.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Repositories
{
    public class RoomRepository : GenericRepository<Room>
    {
        public RoomRepository(MyRoomDbContext context)
            : base(context)
        {
             this.Context = context;
        }

        public MyRoomDbContext Context { get; private set; }

    }
}