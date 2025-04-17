using CinemaApp.Data.Dto;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboard _dashboard;
        public DashboardController(IDashboard dashboard) {
            _dashboard = dashboard;
        }
        public async Task<IActionResult> GetAllMovies(int pg = 1)
        {
            //var mmymovies = await _dashboard.GetAllMovies(pg);
            var (DashboardIndexDto, p) = await _dashboard.GetAllMovies(pg);
            
           
            ViewBag.Pager = p;
            
            return View(DashboardIndexDto);
        }
    }
}
