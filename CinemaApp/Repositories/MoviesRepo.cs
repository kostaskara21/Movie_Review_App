using CinemaApp.Data;
using CinemaApp.Data.Dto;
using CinemaApp.Helpers;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CinemaApp.Repositories
{
    public class MoviesRepo : IMovies
    {

        private readonly ApplicationDbContext _context;
        
        public MoviesRepo(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            
        }

      

        //For Details(Giving spicific movie)
        public async Task<Movies> GetMovieById(int id)
        {
            //Movie we want to Display Details
            var Movie = await _context.Movies.Include(x=>x.Reviews).FirstOrDefaultAsync(x => x.Id == id);
            if (Movie == null) {
                return null;
            }
            return Movie;

        }

        //For Detaisl(Giving the List of related movies)
        public async Task<List<Movies>> GetRelatedMovies(int id)
        {
            //Relared Movies
            //Include is for lazy loading for adding the revies of each movie and then include is for being able to acces the Appuser obj
            var MoreMovies = await _context.Movies.Include(x=>x.Reviews).ThenInclude(i=>i.AppUser).Take(3).ToListAsync();
            
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

        public async Task<(List<Movies> movies, Pager p)> Index(int pg = 1)
        {
            var movies = await _context.Movies.ToListAsync();
            const int pageSize = 6;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = movies.Count;
            var pager = new Pager(recsCount, pg, pageSize); // This should now correctly initialize
            int recSkip = (pg - 1) * pageSize;
            var data = movies.Skip(recSkip).Take(pager.PageSize).ToList();

            return (data, pager);
        }

    }
}
