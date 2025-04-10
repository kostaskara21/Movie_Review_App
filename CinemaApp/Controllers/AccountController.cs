using System.Threading.Tasks;
using CinemaApp.Data.Dto;
using CinemaApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(AccountLogInDto accountLogInDto)
        {
            if (!ModelState.IsValid) { 
                return View(accountLogInDto);
            }
            var user=await _userManager.FindByEmailAsync(accountLogInDto.EmailAdress);
            if (user != null)
            {

                var pass= await _userManager.CheckPasswordAsync(user, accountLogInDto.Password);
                if (pass)
                {
                    var result = await _signinManager.PasswordSignInAsync(user, accountLogInDto.Password, false, false);
                    if (result.Succeeded)
                    {
                        //TempData["LogIn"] = "You are loged In";
                        return RedirectToAction("Index","Movies");
                    }
                }
               
            }
            TempData["LogIn"] = "No Such a user";
            return View(accountLogInDto);
            
        }
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(AccountSignUpDto accountSignUpDto)
        {
            if (!ModelState.IsValid)
            {
                return View(accountSignUpDto);
            }
            var user = await _userManager.FindByEmailAsync(accountSignUpDto.EmailAdress);
            if (user != null)
            {
                TempData["SignUpErrror"] = "This Email Already Exists";
               
            }
            var appuser = new AppUser
            {
                FirstName = accountSignUpDto.FirstName,
                LastName = accountSignUpDto.LastName,
                UserName=accountSignUpDto.EmailAdress,
                Email= accountSignUpDto.EmailAdress
            };
            var newuser = await _userManager.CreateAsync(appuser, accountSignUpDto.Password);
            if (newuser.Succeeded) {
                await _userManager.AddToRoleAsync(appuser, "User");
                TempData["SignUpNoError"] = "User Created Successfully.Please Sign In to use the Platform";
                return View();
            }
            TempData["SignUpErrror"] = "Couldnt Create User Try Again(Pass must include at least one none alphanoumeric and number)";
            //return RedirectToAction("Index", "Home");
            return View();

        }

        
        public async Task<IActionResult> LogOut()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index","Home");

        }

    }
}
