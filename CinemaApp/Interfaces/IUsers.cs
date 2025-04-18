using System.Runtime.CompilerServices;
using CinemaApp.Data.Dto;
using CinemaApp.Helpers;
using CinemaApp.Models;

namespace CinemaApp.Interfaces
{
    public interface IUsers
    {

        Task<List<AppUser>> Index();
        Task<(List<AppUser> movies, Pager p)> Index(int pg);

        Task<List<Movies>> GetUsersMovies(string id);

        Task<bool> UpdateAsync(AppUser user);
        Task<bool> SaveAsync();

        public Task<AppUser> GetById(string id);
        public  Task<AppUser> GetByIdNoTracking(string id);




    }
}
