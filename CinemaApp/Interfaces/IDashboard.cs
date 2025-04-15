using CinemaApp.Helpers;
using CinemaApp.Models;

namespace CinemaApp.Interfaces
{
    public interface IDashboard
    {
        Task<List<Movies>> GetAllMovies();
        Task<(List<Movies> movies, Pager p)> GetAllMovies(int pg);

    }
}
