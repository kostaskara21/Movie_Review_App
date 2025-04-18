using System.Security.Claims;
using System.Threading.Tasks;
using CinemaApp.Data.Dto;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsers _users;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPhotoService _photoService;

        public UsersController(IUsers users, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IPhotoService photoService)
        {
            _users = users;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _photoService = photoService;
        }

        private void MapUserEdi(AppUser appuser, UserEditDto userEditDt, ImageUploadResult imageUploadResult)
        {
            appuser.Id = userEditDt.Id;
            appuser.FirstName = userEditDt.FirstName;
            appuser.LastName = userEditDt.LastName;
            appuser.ImagUrl = imageUploadResult.Url.ToString();
        }
        private void MapUserEditNoImage(AppUser appuser, UserEditDto userEditDt)
        {
            appuser.Id = userEditDt.Id;
            appuser.FirstName = userEditDt.FirstName;
            appuser.LastName = userEditDt.LastName;
            appuser.ImagUrl = null;

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
                    Username = user.UserName,
                    Img=user.ImagUrl
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
            
            var movies = await _users.GetUsersMovies(id);
            var DetailUserDto = new UsersDetailDto
            {

                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Movies = movies,
                Img = user.ImagUrl
            };
            return View(DetailUserDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id,UserEditDto userEditDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","Something went wrong Please try Again");
                return View(userEditDto);

            }
             
            var user = await _users.GetByIdNoTracking(userEditDto.Id);
            if (user == null)
            {
                return NotFound();
            }
            if(user.ImagUrl == null)
            {
                if (userEditDto.Image != null)
                {
                    var resulturl = await _photoService.AddPhotoAsync(userEditDto.Image);
                    //Using this because we are manipulating 2 AppUsers in the same function
                    MapUserEdi(user, userEditDto, resulturl);
                }
                else
                {
                    MapUserEditNoImage(user, userEditDto);
                }
                    await _users.UpdateAsync(user);
                return RedirectToAction("GetAllMovies", "Dashboard");

            }
            else
            {
                
                try
                {
                    var imgcloudinary = await _photoService.DeletePhotoAsync(user.ImagUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete the Photo");
                    return View();

                }
                if (userEditDto.Image != null )
                {
                    var resulturl = await _photoService.AddPhotoAsync(userEditDto.Image);
                    //Using this because we are manipulating 2 AppUsers in the same function
                    MapUserEdi(user, userEditDto, resulturl);
                }
                else
                {
                    MapUserEditNoImage(user, userEditDto);
                }
                await _users.UpdateAsync(user);
               
                return RedirectToAction("GetAllMovies","Dashboard");
                
            }
               
           
        }

        public async Task<IActionResult> Edit(string id)
        {
            //from here we are getting the users id from the http context  
            var cusreid = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user=await _userManager.FindByIdAsync(cusreid);
            if (user == null)
            {
                return NotFound();
            }
            var UserDto = new UserEditDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ImageUrl = user.ImagUrl
,
            };
            return View(UserDto);
        }

    }
}
