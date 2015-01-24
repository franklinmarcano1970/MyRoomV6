using MyRoom.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyRoom.Data.Repositories
{
    public class CategoryProductRepository : GenericRepository<CategoryProduct>
    {
        public MyRoomDbContext Context { get; private set; }
        public CategoryProductRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }

        public void InsertCategoryProduct(List<Category> categories)
        {
            CategoryRepository categoryRepo = new CategoryRepository(this.Context);

            this.DeleteCategorProduct(categories[0].CategoryId);
            if (categories[0].CategoryId != 0)
            {
                categories.ForEach(delegate(Category category)
                {
                    categoryRepo.Insert(category);
                });
            }
        }

        public void DeleteCategorProduct(int categoryId)
        {
            List<CategoryProduct> categories = this.Context.CategoryProducts.Where(c => c.IdCategory == categoryId).ToList();
            try
            {
                this.DeleteCollection(categories);
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