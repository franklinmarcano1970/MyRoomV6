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

        public ActiveHotelProduct GetProductsByHotelId(int hotelId, int prodId)
        {
            var products = this.Context.ActiveHotelProduct.Where(e => e.IdHotel == hotelId && e.IdProduct == prodId).Include("Product");
            if (products.Count() > 0)
                return products.First();
            return null;

        }

        public List<Product> GetProductsByHotelId(int hotelId)
        {
            var hotelProducts = this.Context.ActiveHotelProduct.Include("Product").Where(e => e.IdHotel == hotelId).OrderBy(p => p.Product.Name).ToList();
            List<Product> products = new List<Product>();
            foreach (ActiveHotelProduct hotelProduct in hotelProducts)
            {
                products.Add(hotelProduct.Product);
            }
            return products;
        }

        public List<ActiveHotelProduct> GetActiveProductsByHotelId(int hotelId)
        {
            var hotelProducts = this.Context.ActiveHotelProduct.Include("Product").Where(e => e.IdHotel == hotelId).OrderBy(p => p.Product.Name).ToList();
            List<ActiveHotelProduct> products = new List<ActiveHotelProduct>();
            //foreach (ActiveHotelProduct hotelProduct in hotelProducts)
            //{
            //    products.Add(hotelProduct);
            //}
            return hotelProducts;
        }

        public void InsertActiveHotelProduct(List<ActiveHotelProduct> items, int hotelId)
        {
            this.DeleteActiveHotelProduct(hotelId);

            if (items.Count > 0)
            {           
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