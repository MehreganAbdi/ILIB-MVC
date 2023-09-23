using ILIb1._1.InterFaces;
using ILIb1._1.Models;
using ILIb1._1.ViewModels;
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
        public AdminDashBoardController(ILoanRepository loanRepository, UserManager<AppUser> userManager,
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

        public async Task<IActionResult> Members(string searching)
        {
            if (User.IsInRole("admin"))
            {
                var model = await _userManager.Users.ToListAsync();
                if (searching == null)
                {
                    return View(model);
                }
                var modelAfterSearch =  model.Where(p => p.Email.Contains(searching) 
                                                    || p.UserName.Contains(searching) 
                                                    || p.PhoneNumber.Contains(searching)).ToList();
                return View(modelAfterSearch);
            }
            return RedirectToAction("Index", "Book");
        }

        public async Task<IActionResult> DeleteMember(string Id)
        {
             _loanRepository.RemoveMember(Id);
            return RedirectToAction("Index");
            
        }
        public async Task<IActionResult> Loans()
        {
            if (User.IsInRole("admin"))
            {
                var loans = await _loanRepository.GetAll();
                var model = new List<LoanVM>();
                foreach (var item in loans)
                {
                    model.Add(await _loanRepository.GetLoanDetail(item));
                }
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

                return RedirectToAction("Index", "AdminDashBoard");
            }
            return RedirectToAction("Index", "Book");



        }

    }
}
