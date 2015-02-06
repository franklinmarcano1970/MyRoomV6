using MyRoom.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyRoom.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public MyRoomDbContext Context { get; private set; }
        public CategoryRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }


        public override void Update(Category entity)
        {
            foreach (Module item in entity.Modules)
            {
                this.Context.Entry(item).State = EntityState.Modified;
            }
        }

        public override async System.Threading.Tasks.Task EditAsync(Category entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Context.Entry(entity.Translation).State = EntityState.Modified;
            await this.Context.SaveChangesAsync();
        }

        public List<Category> GetByParentId(int categoryId)
        {
            return (from p in this.Context.Categories.Include("Translation")
                    where p.IdParentCategory == categoryId
                    orderby p.IdParentCategory, p.Orden
                    select p).ToList<Category>();
        }

        public void ModuleStateUnchange(Category entity)
        {
            foreach (Module item in entity.Modules)
            {
                this.Context.Entry(item).State = EntityState.Unchanged;
            }
        }
    }
}