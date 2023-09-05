using RunGroopWebApp.Data.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        public Task<List<Club>> GetAllUserClubs()
        {
            throw new NotImplementedException();
        }

        public Task<List<Race>> GetAllUserRaces()
        {
            throw new NotImplementedException();
        }
    }
}
