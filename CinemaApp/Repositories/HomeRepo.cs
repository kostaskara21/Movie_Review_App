using CinemaApp.Data;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Repositories
{
    public class HomeRepo : IHome
    {
        private readonly ApplicationDbContext _context;
        public HomeRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Movies>> GetMoviesThreeMovies()
        {
            var movies=await _context.Movies.Take(3).ToListAsync();
            return movies;
        }

       
    }
}
