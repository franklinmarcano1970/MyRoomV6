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
    public class ActiveHotelModuleRepository : GenericRepository<ActiveHotelModule>
    {
        public MyRoomDbContext Context { get; private set; }
        public ActiveHotelModuleRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }

        public void InsertActiveHotelModule(List<ActiveHotelModule> items, int hotelId, bool deleteActiveModules=false)
        {
            if (deleteActiveModules)
            { 
                this.DeleteActiveHotelModule(hotelId);
            } 
            
            if (items.Count > 0)
            {
                items.ForEach(delegate(ActiveHotelModule module)
                {
                        this.Insert(new ActiveHotelModule()
                        {
                            IdHotel = module.IdHotel, 
                            IdModule =  module.IdModule,
                            Active = true,
                        });
                });
            }
        }

        public void DeleteActiveHotelModule(int hotelId)
        {
            List<ActiveHotelModule> hotels = this.Context.ActiveHotelModule.Where(c => c.IdHotel == hotelId).ToList();
            try
            {
                this.DeleteCollection(hotels);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}