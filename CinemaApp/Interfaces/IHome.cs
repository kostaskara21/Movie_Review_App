using CinemaApp.Models;

namespace CinemaApp.Interfaces
{
    public interface IHome
    {
        public Task<List<Movies>> GetMoviesThreeMovies();
    }
}
