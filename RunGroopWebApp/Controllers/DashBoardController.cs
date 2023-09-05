using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;

namespace RunGroopWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context, IUserService)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
