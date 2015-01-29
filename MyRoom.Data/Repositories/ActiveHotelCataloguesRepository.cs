using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyRoom.Data.Repositories
{
    public class ActiveHotelCatalogRepository : GenericRepository<ActiveHotelCatalogue>
    {
        public MyRoomDbContext Context { get; private set; }
        public ActiveHotelCatalogRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }

        public void InsertActiveHotelCatalogues(List<ActiveHotelCatalogue> hotelCatalogues)
        {
            this.DeleteActiveHotelCatalogues(hotelCatalogues[0].IdHotel);
            if (hotelCatalogues[0].IdHotel != 0)
            {
                hotelCatalogues.ForEach(delegate(ActiveHotelCatalogue hotelCatalog)
                {
                    this.Insert(hotelCatalog);
                });
            }
        }

        public void DeleteActiveHotelCatalogues(int hotelId)
        {
            List<ActiveHotelCatalogue> hotelCatalogues = this.Context.HotelCatalogues.Where(c => c.IdHotel == hotelId).ToList();
            try
            {
                this.DeleteCollection(hotelCatalogues);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public IQueryable<Permission> GetById(string id)
        //{
        //    return this.Context.Permissions.Where(c => c.IdUser == id);
        //}
    }
}