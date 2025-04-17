using System.Threading.Tasks;
using CinemaApp.Data.Dto;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsers _users;
        public UsersController(IUsers users) {
            _users = users;
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
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.UserName
                };
                AllUsers.Add(DtoUser);
            }
            ViewBag.Pager = p;
            return View(AllUsers);
        }
    }
}
