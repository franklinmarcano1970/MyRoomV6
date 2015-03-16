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
    public class UserCategoryRepository : GenericRepository<RelUserCategory>
    {
        public MyRoomDbContext Context { get; private set; }
        public UserCategoryRepository(MyRoomDbContext context)
            : base(context)
        {
            this.Context = context;
        }

        public void InsertUserCategory(List<RelUserCategory> items, string userId, bool deleteUserCategories = false)
        {
            if (deleteUserCategories)
            {
                this.DeleteUserCategory(userId);
            }

            if (items.Count > 0)
            {
                items.ForEach(delegate(RelUserCategory userCategory)
                {
                    this.Insert(userCategory);
                });
            }
        }

        public void DeleteUserCategory(string userId)
        {
            List<RelUserCategory> userCategories = this.Context.RelUserCategory.Where(c => c.IdUser == userId).ToList();
            try
            {
                this.DeleteCollection(userCategories);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}