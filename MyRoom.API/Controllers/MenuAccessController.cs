using MyRoom.Data;
using MyRoom.Data.Repositories;
using MyRoom.Model;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace MyRoom.API.Controllers
{
    public class MenuAccessController : ODataController
    {
        private GenericRepository<MenuAccess> menuAccessRepository = new GenericRepository<MenuAccess>(new MyRoomDbContext());


        // GET: odata/MenuAccess
        [AllowAnonymous]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<MenuAccess> GetMenuAccess()
        {
            return menuAccessRepository.GetAll();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                menuAccessRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
