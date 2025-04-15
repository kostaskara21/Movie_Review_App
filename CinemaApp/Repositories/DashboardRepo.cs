using CinemaApp.Data;
using CinemaApp.Helpers;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Repositories
{
    public class DashboardRepo : IDashboard
    {
        private readonly ApplicationDbContext _context;
        //We use this because the UserManager can not be used in the repository 
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public DashboardRepo(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            
        }

        

      

        public async Task<List<Movies>> GetAllMovies()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var userid=_userManager.GetUserId(user);
            var MyMOvies = await _context.Movies.Where(i => i.AppUserId == userid).ToListAsync();
            return MyMOvies;
        }

        public async Task<(List<Movies> movies, Pager p)> GetAllMovies(int pg = 1)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var userid = _userManager.GetUserId(user);
            var movies = await  _context.Movies.Where(i => i.AppUserId == userid).ToListAsync();
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
