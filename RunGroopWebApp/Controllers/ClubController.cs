using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Data.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoServices _photoServices;
        //adding functionality for the server to return app UserID
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public ClubController(IClubRepository clubRepository, IPhotoServices photoServices, IHttpContextAccessor httpcontextAccessor)
        {
            _clubRepository = clubRepository;
            _photoServices = photoServices;
            _httpcontextAccessor = httpcontextAccessor;
        }
        //Returning a List
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }

        //returning Details through id
        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }
        public async Task<IActionResult> Create()
        {
            //retrieves httpcontext userId
            var currentUserId = _httpcontextAccessor.HttpContext.User.GetUserId();
            var createClubViewModel = new CreateClubViewModel { AppUserId = currentUserId };
            return View(createClubViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoServices.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    UserId = clubVM.AppUserId,
                    ClubCategory = clubVM.ClubCategory,
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        Province = clubVM.Address.Province,
                    }
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo uplaod failed!");
            }

            return View(clubVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.Image,
                ClubCategory = club.ClubCategory
            };
            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", clubVM);
            }

            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);

            if (userClub != null)
                try
                {
                    await _photoServices.DeletePhotoAsync(userClub.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(clubVM);
                }

            var photoResult = await _photoServices.AddPhotoAsync(clubVM.Image);

            var club = new Club
            {
                Id = id,
                Title = clubVM.Title,
                Description = clubVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = clubVM.AddressId,
                Address = clubVM.Address
            };

            _clubRepository.Update(club);

            return RedirectToAction("Index");
        }


            //else
            //{
            //    return View(clubVM);
            //}

        public async Task<IActionResult>Delete(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");

            _clubRepository.Delete(clubDetails);
            return RedirectToAction("Index");
        }
    }
}

    

