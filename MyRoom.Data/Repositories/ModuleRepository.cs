using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyRoom.Data.Repositories
{
    public class ModuleRepository : GenericRepository<Module>
    {
        public MyRoomDbContext Context  { get; private set; }
        public ModuleRepository(MyRoomDbContext context) : base(context)
        {
            this.Context = context;
        }


        public void CatalogStateUnchange(Module entity)
        {
            foreach (Catalog item in entity.Catalogues)
            {
                this.Context.Entry(item).State = EntityState.Unchanged;
            }
        }

        public override async System.Threading.Tasks.Task EditAsync(Module entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Context.Entry(entity.Translation).State = EntityState.Modified;
            await this.Context.SaveChangesAsync();

        }
    }
}