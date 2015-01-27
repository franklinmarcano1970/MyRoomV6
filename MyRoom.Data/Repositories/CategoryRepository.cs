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

        public List<Category> GetByParentId(int categoryId)
        {
            return (from p in this.Context.Categories
                    where p.IdParentCategory == categoryId
                    orderby p.IdParentCategory, p.Orden
                    select p).ToList<Category>();
            }


        //public List<Category> GetCategoriesParents(int categoryId)
        //{

        //    List<Category> categories = (from c in this.Context.Categories.Include("Translation")
        //                                                       .Include("Products")
        //                                where c.IdParentCategory == categoryId
        //                                select c).ToList();
        //    return categories;

        //}
        
        //public List<Category> GetCategoriesChildren(Category categoryParent)
        //{
        //    Category category = null;
        //    Category nextChild = null;
        //    int index = 0;
        //    int categoryId = categoryParent.CategoryId;
            
        //    List<Category> categories = this.Context.Categories.Include("Translation")
        //                                                       .Include("Products")
        //                                                       .Where(c => c.CategoryItem == categoryParent.CategoryItem && c.Active == true && c.CategoryId != categoryParent.CategoryId)
        //                                                       .ToList();

        //    foreach (Category cat in categories)
        //    {

        //        List<Category> parentCategory = (from p  in this.Context.Categories
        //                                         where p.IdParentCategory == cat.IdParentCategory
        //                                        select p).ToList();

        //        categoryParent.Children = parentCategory;
        //        //if (c.IdParentCategory == categoryId)
        //        //{
        //        //    if (category == null)
        //        //    {
        //        //        category = c;
        //        //        if (index != categories.Count() - 1)
        //        //            category.CategoryChildren = new Category();

        //        //        nextChild = category.CategoryChildren;
        //        //    }
        //        //    else
        //        //    {
        //        //        nextChild.Active = c.Active;
        //        //        nextChild.Name = c.Name;
        //        //        nextChild.CategoryId = c.CategoryId;
        //        //        nextChild.CategoryItem = c.CategoryItem;
        //        //        nextChild.Comment = c.Comment;
        //        //        nextChild.IdParentCategory = c.IdParentCategory;
        //        //        nextChild.IdTranslationName = c.IdTranslationName;
        //        //        nextChild.Image = c.Image;
        //        //        nextChild.IsFinal = c.IsFinal;
        //        //        nextChild.IsFirst = c.IsFirst;
        //        //        nextChild.Orden = c.Orden;
        //        //        nextChild.Pending = c.Pending;
        //        //        nextChild.Prefix = c.Prefix;
        //        //        nextChild.Translation = c.Translation;
        //        //        nextChild.Modules = c.Modules;
        //        //        nextChild.ActiveHotelCategory = c.ActiveHotelCategory;


        //        //        if (index != categories.Count() - 1)
        //        //            nextChild.CategoryChildren = new Category();
        //        //        nextChild = nextChild.CategoryChildren;
        //        //    }

        //        //    //nextChild.CategoryChildren.CategoryChildren = new Category();                                                
        //        //}
        //        //else
        //        //{
        //        //    if (nextChild != null)
        //        //    {
        //        //        nextChild.Active = c.Active;
        //        //        nextChild.Name = c.Name;
        //        //        nextChild.CategoryId = c.CategoryId;
        //        //        nextChild.CategoryItem = c.CategoryItem;
        //        //        nextChild.Comment = c.Comment;
        //        //        nextChild.IdParentCategory = c.IdParentCategory;
        //        //        nextChild.IdTranslationName = c.IdTranslationName;
        //        //        nextChild.Image = c.Image;
        //        //        nextChild.IsFinal = c.IsFinal;
        //        //        nextChild.IsFirst = c.IsFirst;
        //        //        nextChild.Orden = c.Orden;
        //        //        nextChild.Pending = c.Pending;
        //        //        nextChild.Prefix = c.Prefix;
        //        //        nextChild.Translation = c.Translation;
        //        //        nextChild.Modules = c.Modules;
        //        //        nextChild.ActiveHotelCategory = c.ActiveHotelCategory;


        //        //        if (index != categories.Count() - 1)
        //        //            nextChild.CategoryChildren = new Category();
        //        //        nextChild = nextChild.CategoryChildren;
        //        //    }
        //        }
        //        //categoryId = c.CategoryId;
        //        //index++;
        //    //}
        //    return new List<Category>();
        //  //  return category;
        //}

        public void ModuleStateUnchange(Category entity)
        {
            foreach (Module item in entity.Modules)
            {
                this.Context.Entry(item).State = EntityState.Unchanged;
            }
        }
    }
}