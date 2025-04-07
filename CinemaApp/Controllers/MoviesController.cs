using CinemaApp.Data;
using CinemaApp.Data.Dto;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using CinemaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovies _movies;
        private readonly IPhotoService _photoService;

        private readonly ApplicationDbContext _context;
        


        public MoviesController(ApplicationDbContext context, IMovies movies, IPhotoService photoService)
        {
            _context = context;
            _movies = movies;
            _photoService = photoService;
        }


        public async Task<IActionResult> Index()
        {
            //var Movies=_context.Movies.ToList();
            var Movies= await _movies.Index();
            return View(Movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            //We are using the Repo and getting Back A View Model that we made (for passing to the View both List and also Single Movie)
            //We mostly care about the Specific movie thats why we are only ckecking one attribut of the ModelView
            var MovieDetailViewModel= await _movies.Details(id);
            if (MovieDetailViewModel.MyMovie == null)
            {
                return NotFound();
            }

            //Passing this to the View
            return View(MovieDetailViewModel);
        }


        public async Task<IActionResult> Create()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateDto movies)
        {
           
            if (ModelState.IsValid)
            {
                
                var result = await _photoService.AddPhotoAsync(movies.Image);
                var movie = new Movies
                {
                    Title = movies.Title,
                    Description = movies.Description,
                    Image = result.Url.ToString(),
                    Categories = movies.Categories
                };
                _movies.Add(movie);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo could not Upload ");
            }
            return View(movies);
           
        }

        public async Task<IActionResult> Edit()
        {

            return View();
        }





    }
}
