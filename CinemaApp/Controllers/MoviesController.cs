using CinemaApp.Data;
using CinemaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var Movies=_context.Movies.ToList();
            return View(Movies);
        }

        public IActionResult Details(int id)
        {
            //Movie we want to Display Details
            var DetailMovie=_context.Movies.FirstOrDefault(x => x.Id == id);

            //Relared Movies
            var MoreMovies=_context.Movies.Where(x=>x.Id != id).Take(5).ToList();
            
            //The ViewModel we made
            var ViewModel = new MovieDetailViewModel
            {
                MyMovie = DetailMovie,
                MoreMoviesM = MoreMovies
            };

            //Passing this to the View
            return View(ViewModel);
        }
    }
}
