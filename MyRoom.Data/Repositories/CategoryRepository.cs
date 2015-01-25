using MyRoom.Model;
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



        internal Category GetCategoriesChildren(int categoryId)
        {
            List<Category> categories = this.Context.Categories.Include("Translation")
                                                               .Include("Products")
                                                               .Where(c => c.CategoryItem == categoryId && c.Active == true)
                                                               .ToList();

            Category category = null;
            Category nextChild = null;
            int index = 0;
            foreach (Category c in categories)
            {

                if (c.IdParentCategory == categoryId)
                {
                    if (category == null)
                    {
                        category = c;
                        if (index != categories.Count() - 1)
                            category.CategoryChildren = new Category();

                        nextChild = category.CategoryChildren;
                    }
                    else
                    {
                        nextChild.Active = c.Active;
                        nextChild.Name = c.Name;
                        nextChild.CategoryId = c.CategoryId;
                        nextChild.CategoryItem = c.CategoryItem;
                        nextChild.Comment = c.Comment;
                        nextChild.IdParentCategory = c.IdParentCategory;
                        nextChild.IdTranslationName = c.IdTranslationName;
                        nextChild.Image = c.Image;
                        nextChild.IsFinal = c.IsFinal;
                        nextChild.IsFirst = c.IsFirst;
                        nextChild.Orden = c.Orden;
                        nextChild.Pending = c.Pending;
                        nextChild.Prefix = c.Prefix;
                        nextChild.Translation = c.Translation;
                        nextChild.Modules = c.Modules;
                        nextChild.ActiveHotelCategory = c.ActiveHotelCategory;
                   

                        if (index != categories.Count() - 1)
                            nextChild.CategoryChildren = new Category();
                        nextChild = nextChild.CategoryChildren;
                    }

                    //nextChild.CategoryChildren.CategoryChildren = new Category();                                                
                }
                else
                {
                    if (nextChild != null)
                    {
                        nextChild.Active = c.Active;
                        nextChild.Name = c.Name;
                        nextChild.CategoryId = c.CategoryId;
                        nextChild.CategoryItem = c.CategoryItem;
                        nextChild.Comment = c.Comment;
                        nextChild.IdParentCategory = c.IdParentCategory;
                        nextChild.IdTranslationName = c.IdTranslationName;
                        nextChild.Image = c.Image;
                        nextChild.IsFinal = c.IsFinal;
                        nextChild.IsFirst = c.IsFirst;
                        nextChild.Orden = c.Orden;
                        nextChild.Pending = c.Pending;
                        nextChild.Prefix = c.Prefix;
                        nextChild.Translation = c.Translation;
                        nextChild.Modules = c.Modules;
                        nextChild.ActiveHotelCategory = c.ActiveHotelCategory;
                  

                        if (index != categories.Count() - 1)
                            nextChild.CategoryChildren = new Category();
                        nextChild = nextChild.CategoryChildren;
                    }
                }
                categoryId = c.CategoryId;
                index++;
            }

            return category;
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