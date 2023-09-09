using RunGroopWebApp.Data;
using RunGroopWebApp.Data.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = _context.Clubs.Where(r => r.User.Id == currentUser);
            return userClubs.ToList();
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userRaces = _context.Races.Where(r => r.User.Id == currentUser);
            return userRaces.ToList();
        }
        public async Task<User> GetUserById(string id)
        {

            return await _context.Users.FindAsync(id);

        }
    }
}
