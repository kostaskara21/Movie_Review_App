using System.Threading.Tasks;
using CinemaApp.Data.Dto;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsers _users;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(IUsers users, UserManager<AppUser> userManager)
        {
            _users = users;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index(int pg = 1)
        {
            //var Movies=_context.Movies.ToList();
            var (AppUsers, p) = await _users.Index(pg);
            //In all the privious Indexes we were returning list but we were not using Dtos(we were dipslaying str8 the model)
            //Now we want to pass a list of Dtos in our model so we need for each on of the list of the users we are getting
            //to make the mapper with Dto and pass them 
            var AllUsers = new List<UsersIndexDto>();
            foreach (var user in AppUsers)
            {
                var DtoUser = new UsersIndexDto
                {
                    id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.UserName
                };
                AllUsers.Add(DtoUser);
            }
            ViewBag.Pager = p;
            return View(AllUsers);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            //var CurrentUser = await _userManager.GetUserAsync(User);
            var movies = await _users.GetUsersMovies(id);
            var DetailUserDto = new UsersDetailDto
            {

                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Movies = movies,
            };
            return View(DetailUserDto);
        }

    }
}
