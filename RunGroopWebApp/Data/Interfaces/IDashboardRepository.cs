using RunGroopWebApp.Models;

namespace RunGroopWebApp.Data.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();
        Task<User> GetUserById(string id);
    }
}
