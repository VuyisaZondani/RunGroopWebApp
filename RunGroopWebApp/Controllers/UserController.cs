﻿using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data.Interfaces;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Pace = user.Pace,
                    Kilos = user.Kilos
                };
                result.Add(userViewModel);
            }
            return View(result);
        }
    }
}
