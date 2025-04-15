using System.Diagnostics;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHome _home;

        public HomeController(ILogger<HomeController> logger, IHome home)
        {
            _logger = logger;
            _home = home;
        }

        public async Task<IActionResult> Index()
        {
           var movies=await _home.GetMoviesThreeMovies();
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
