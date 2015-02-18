using MyRoom.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>
    {
        public DepartmentRepository(MyRoomDbContext context)
            : base(context)
        {
             this.Context = context;
        }

        public override Department GetById(object id)
        {
            return (from c in this.Context.Departments.Include("Translation")
                         where c.DepartmentId == (int)id
                         select c).First();

        }

        public string GetDepartmentyId(int id)
        {
            var hotel = (from c in this.Context.Hotels.Include("Translation")
                         where c.HotelId == id
                         select c).First();

            string json = JsonConvert.SerializeObject(hotel, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
            return json;
        }

        public override async System.Threading.Tasks.Task EditAsync(Department entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Context.Entry(entity.Translation).State = EntityState.Modified;
            await this.Context.SaveChangesAsync();

        }
        public MyRoomDbContext Context { get; private set; }

    }
}