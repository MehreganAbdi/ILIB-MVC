using ILIb1._1.InterFaces;
using ILIb1._1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ILIb1._1.Controllers
{
    public class AdminDashBoardController : Controller
    {
        private readonly ILoanRepository _loanRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;

        public AdminDashBoardController(ILoanRepository loanRepository , UserManager<AppUser> userManager,
                                    SignInManager<AppUser> signInManager)
        {
            _loanRepository = loanRepository;
            _signinManager = signInManager;
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Members()
        {
            if (User.IsInRole("admin"))
            {
                var model = await _userManager.Users.ToListAsync();
                return View(model);
            }
            return RedirectToAction("Index", "Book");
        } 
    }
}
