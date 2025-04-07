using CinemaApp.Data;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using CinemaApp.ViewModels;
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

        
        //This is for makign the MoviedetailsModelView and returning it in the controller
        //So we do not make our cotroller bigger and also proving loose coupling 
        public async Task<MovieDetailViewModel> Details(int id)
        {
            //Movie we want to Display Details
            var DetailMovie = await this.GetMovieById(id);

            //Relared Movies
            var MoreMovies = await this.GetRelatedMovies(id);
            
            //We are making the model 
            return  new MovieDetailViewModel
            {
                MyMovie = DetailMovie,
                MoreMoviesM = MoreMovies
            };
        }

        //For Details(Giving spicific movie)
        public async Task<Movies> GetMovieById(int id)
        {
            //Movie we want to Display Details
            var Movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<Movies> Edit(int id)
        {
           var movie=await this.GetMovieById(id);
            if (movie == null)
            {
                return null;
            }
            return movie;
        }

        public bool Update(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
