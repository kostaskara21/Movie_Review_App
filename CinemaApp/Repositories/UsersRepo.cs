using CinemaApp.Data;
using CinemaApp.Data.Dto;
using CinemaApp.Helpers;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Repositories
{
    public class UsersRepo : IUsers
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersRepo(ApplicationDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<AppUser>> Index()
        {
            ///with the http contect we are accesing the http context to get the current user
            ///with the repo pattern we can not depend direcltyu to the userManager
            var user = _httpContextAccessor.HttpContext?.User;
            var userid = _userManager.GetUserId(user);
            if (userid == null)
            {
                return null;
            }
            //We want all the users expect from the users who is signed in 
            var users = await _context.AppUsers.Where(u => u.Id != userid).ToListAsync();
            return users;

        }

        public async Task<(List<AppUser> movies, Pager p)> Index(int pg)
        {
            ///with the http contect we are accesing the http context to get the current user
            ///with the repo pattern we can not depend direcltyu to the userManager
            var user = _httpContextAccessor.HttpContext?.User;
            var userid = _userManager.GetUserId(user);

            //We want all the users expect from the users who is signed in 
            var users = await _context.AppUsers.Where(u => u.Id != userid).ToListAsync();
            const int pageSize = 6;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = users.Count;
            var pager = new Pager(recsCount, pg, pageSize); // This should now correctly initialize
            int recSkip = (pg - 1) * pageSize;
            var data = users.Skip(recSkip).Take(pager.PageSize).ToList();

            return (data, pager);

        }






        public async Task<List<Movies>> GetUsersMovies(string id)
        {
            var user = _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            var movies = await _context.Movies.Where(x => x.AppUserId == id).ToListAsync();
            return movies;
        }

       
    }
}
