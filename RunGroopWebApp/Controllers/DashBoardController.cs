using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Data.Interfaces;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoServices _photoServices;

        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor,
            IPhotoServices photoServices)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoServices = photoServices;
        }
        public async  Task<IActionResult> Index()
        {
            var userRaces = await _dashboardRepository.GetAllUserRaces();
            var userClubs = await _dashboardRepository.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Races = userRaces,
                Clubs = userClubs
            };
            return View(dashboardViewModel);
        }
        public  async Task<IActionResult> EditUserProfile()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(currentUserId);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = currentUserId,
                Pace = user.Pace,
                Kilos = user.Kilos,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                Province = user.Province
            };
            return View(editUserViewModel);
        }
    }
}
