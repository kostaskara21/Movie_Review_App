using CinemaApp.Data.Dto;
using CinemaApp.Helpers;
using CinemaApp.Models;

namespace CinemaApp.Interfaces
{
    public interface IDashboard
    {
        Task<List<Movies>> GetAllMovies();
        Task<(DashboardIndexDto movies, Pager p)> GetAllMovies(int pg);

    }
}
