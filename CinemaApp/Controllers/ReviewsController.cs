using CinemaApp.Data.Dto;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Controllers
{
    public class ReviewsController : Controller
    {

        private readonly IReview _review;
        private readonly UserManager<AppUser> _userManager;
        public ReviewsController(IReview review,UserManager<AppUser> userManager)
        {
            _review = review;
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> Create(ReviewsCreateDtos reviewsCreateDtos)
        {
            var userId = _userManager.GetUserId(User); // Gets the Id of the logged in user
            if (userId == null) {
                return NotFound();

            }
            if (!ModelState.IsValid) {
                return NotFound();
            }
            var Review = new Reviews
            {
                Comment = reviewsCreateDtos.Comment,
                MovieId = reviewsCreateDtos.MovieId,
                AppUserId = userId,
            };
            _review.Add(Review);
            return  RedirectToAction("Details", "Movies", new { id = reviewsCreateDtos.MovieId }); 
            
        }
    }
}
