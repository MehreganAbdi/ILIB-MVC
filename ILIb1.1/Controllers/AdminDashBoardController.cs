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
        private readonly IBookRepository _bookRepository;
        private readonly SignInManager<AppUser> _signInManager;
        public AdminDashBoardController(ILoanRepository loanRepository , UserManager<AppUser> userManager,
                               SignInManager<AppUser> signInManager, IBookRepository bookRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _userManager = userManager;
            _signInManager = signInManager;

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


        public async Task<IActionResult> Loans()
        {
            if (User.IsInRole("admin"))
            {
                var model = await _loanRepository.GetAll();
                return View(model);
            }
            return RedirectToAction("Index", "Book");

        }

        public async Task<IActionResult> RemainedBooks()
        {
            if (User.IsInRole("admin"))
            {
                var model = await _bookRepository.GetAll();

                model = model
                    .Where(p => p.BookCount > 0).ToList();
                
                return View(model);
            }
            return RedirectToAction("Index", "Book");


        }
    
        
        public async Task<IActionResult> BlockMember(string id)
        {
            if (User.IsInRole("admin"))
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    _userManager.SetLockoutEnabledAsync(user, false);
                }
                
                return RedirectToAction("Index" , "AdminDashBoard");
            }
            return RedirectToAction("Index", "Book");



        }

    }
}
