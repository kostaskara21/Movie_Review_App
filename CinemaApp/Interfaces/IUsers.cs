using CinemaApp.Helpers;
using CinemaApp.Models;

namespace CinemaApp.Interfaces
{
    public interface IUsers
    {

        Task<List<AppUser>> Index();
        Task<(List<AppUser> movies, Pager p)> Index(int pg);
    }
}
