using CinemaApp.Data;
using CinemaApp.Data.Dto;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Repositories
{
    public class MoviesRepo : IMovies
    {

        private readonly ApplicationDbContext _context;
        public MoviesRepo(ApplicationDbContext context)
        {
            _context = context;
        }

      

        //For Details(Giving spicific movie)
        public async Task<Movies> GetMovieById(int id)
        {
            //Movie we want to Display Details
            var Movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (Movie == null) {
                return null;
            }
            return Movie;

        }

        //For Detaisl(Giving the List of related movies)
        public async Task<List<Movies>> GetRelatedMovies(int id)
        {
            //Relared Movies
            var MoreMovies = await _context.Movies.Where(x => x.Id != id).Take(3).ToListAsync();
            return MoreMovies;
        }


        public async Task<List<Movies>> Index()
        {
            return (await _context.Movies.ToListAsync()); 
        }



        public bool Add(Movies movie)
        {
            _context.Add(movie);
            return Save();
        }


        public bool Remove(Movies movie)
        {
            _context.Remove(movie);
            return Save();
        }

        public bool Save()
        {
            var status=_context.SaveChanges();
            if (status > 0)
            {
                return true;
            }
            return false;
        }


        public bool Update(Movies movie)
        {
            _context.Update(movie);
            return Save();
        }

       

       
    }
}
