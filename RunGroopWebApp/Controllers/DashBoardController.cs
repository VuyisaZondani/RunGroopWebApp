using Microsoft.AspNetCore.Mvc;

namespace RunGroopWebApp.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
