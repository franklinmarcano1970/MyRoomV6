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
    public class ActiveHotelProductRepository : GenericRepository<ActiveHotelProduct>
    {
        public MyRoomDbContext Context { get; private set; }
        public ActiveHotelProductRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }

        public List<ActiveHotelProduct> GetProductsByHotelId(int hotelId)
        {
            return this.Context.ActiveHotelProduct.Where(e => e.IdHotel == hotelId).ToList();
        }

        public void InsertActiveHotelProduct(List<ActiveHotelProduct> items)
        {
            this.DeleteActiveHotelProduct(items[0].IdHotel);
            if (items[0].IdHotel != 0)
            {
                items.ForEach(delegate(ActiveHotelProduct product)
                {
                        this.Insert(new ActiveHotelProduct() { 
                            IdHotel = product.IdHotel, 
                            IdProduct =  product.IdProduct,
                            Active = true,
                        });
                });
            }
        }

        public void DeleteActiveHotelProduct(int hotelId)
        {
            List<ActiveHotelProduct> hotels = this.Context.ActiveHotelProduct.Where(c => c.IdHotel == hotelId).ToList();
            try
            {
                this.DeleteCollection(hotels);
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