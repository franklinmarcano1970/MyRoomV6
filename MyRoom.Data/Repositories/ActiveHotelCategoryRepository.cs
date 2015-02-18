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
    public class ActiveHotelCategoryRepository : GenericRepository<ActiveHotelCategory>
    {
        public MyRoomDbContext Context { get; private set; }
        public ActiveHotelCategoryRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }

        public void InsertActiveHotelCategory(List<ActiveHotelCategory> items, int hotelId)
        {
            this.DeleteActiveHotelCategory(hotelId);
            if (items.Count > 0)
            {
                items.ForEach(delegate(ActiveHotelCategory category)
                {
                    this.Insert(new ActiveHotelCategory()
                    {
                            IdHotel = category.IdHotel, 
                            IdCategory =  category.IdCategory,
                            Active = true,
                        });
                });
            }
        }

        public void DeleteActiveHotelCategory(int hotelId)
        {
            List<ActiveHotelCategory> hotels = this.Context.ActiveHotelCategory.Where(c => c.IdHotel == hotelId).ToList();
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