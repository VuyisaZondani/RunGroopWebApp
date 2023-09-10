using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhotoServices _photoServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserRepository userRepository, IPhotoServices photoServices, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _photoServices = photoServices;
            _httpContextAccessor = httpContextAccessor;
        }

        private void MapUserEdit(User user,UserViewModel userViewModel, ImageUploadResult photoResult)
        {
            user.Id = userViewModel.Id;
            user.UserName = userViewModel.UserName;
            user.Pace = userViewModel.Pace;
            user.Kilos = userViewModel.Kilos;
            user.ProfileImageUrl = photoResult.Url.ToString();
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
                    Kilos = user.Kilos,
                    ProfileImageUrl = user.ProfileImageUrl
                   
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Kilos = user.Kilos,
                ProfileImageUrl = user.ProfileImageUrl
                
            };

            return View(userDetailViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _userRepository.GetUserById(currentUserId);

            if (user == null) return View("Error");

            var userViewModel = new UserViewModel()
            {
                Id = currentUserId,
                UserName = user.UserName,
                Pace = user.Pace,
                Kilos = user.Kilos,
                ProfileImageUrl = user.ProfileImageUrl
            };
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserViewModel editProfileViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit Race");
                return View("EditProfile", editProfileViewModel);
            }

            var userProfile = await _userRepository.GetUserByIdNoTracking(editProfileViewModel.Id);

            if (userProfile.ProfileImageUrl == "" || userProfile.ProfileImageUrl == null)
            {
                var photoresult = await _photoServices.AddPhotoAsync(editProfileViewModel.Image);

                MapUserEdit(userProfile, editProfileViewModel, photoresult);

                _userRepository.Update(userProfile);

                return RedirectToAction("Index");

            }

            else
            {
                try
                {
                    await _photoServices.DeletePhotoAsync(userProfile.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editProfileViewModel);
                }
                var photoResult = await _photoServices.AddPhotoAsync(editProfileViewModel.Image);

                MapUserEdit(userProfile, editProfileViewModel, photoResult);

                _userRepository.Update(userProfile);

                return RedirectToAction("Index");
            }
        }
        
        public async Task<IActionResult> Delete(string id)
        {
            var userDetails = await _userRepository.GetUserById(id);
            if (userDetails == null) return View("Error");
            return View(new UserViewModel());
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult>DeleteUser(string id)
        {
            var userDetails = await _userRepository.GetUserById(id);
            if (userDetails == null) return View("error");

            _userRepository.Delete(userDetails);
            return RedirectToAction("Index");
        }
        
    }
}
