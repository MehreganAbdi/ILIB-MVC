using ILIb1._1.Data;
using ILIb1._1.Models;
using ILIb1._1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ILIb1._1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly ApplicationDBContext _context;

        public AccountController(UserManager<AppUser> userManager,
                                    SignInManager<AppUser> signInManager,
                                        ApplicationDBContext applicationDBContext)
        {
            _context = applicationDBContext;
            _signinManager = signInManager;
            _userManager = userManager;

        }
        public IActionResult Login()
        {

            //if we reload the page this will hold previos inserted values.

            var reloadSafety = new LoginVM();
            return View(reloadSafety);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passChecking = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if (passChecking)
                {
                    var res = await _signinManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                    if (res.Succeeded)
                    {
                        return RedirectToAction("Index", "Book");
                    }
                }
                TempData["Error"] = "password must include neccessary conditions";
                return View(loginVM);
            }
            TempData["Error"] = "wrong Inputs";


            return View(loginVM);
        }



        public IActionResult Register()
        {
            //if we reload the page this will hold previos inserted values.

            var reloadSafety = new RegisterVM();
            return View(reloadSafety);

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM); 
            }

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);


            if (user != null)
            {
                TempData["Error"] = "This Email Already Exists";
                return View(registerVM);
            }
            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                 UserName = registerVM.EmailAddress

            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return RedirectToAction("Index", "Book");


        }



        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Book");
        }
    }
}
