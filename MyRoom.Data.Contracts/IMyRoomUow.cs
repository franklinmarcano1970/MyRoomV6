using MyRoom.Model;

namespace MyRoom.Data.Contracts
{
    /// <summary>
    /// Interface for the Code Camper "Unit of Work"
    /// </summary>
    public interface IMyRoomUow
    {
        // Save pending changes to the data store.
        void Commit();
        // Repositories
        IRepository<ApplicationUser> Users { get; }     
    }
}