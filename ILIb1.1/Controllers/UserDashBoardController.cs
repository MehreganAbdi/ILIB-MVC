using ILIb1._1.InterFaces;
using ILIb1._1.Models;
using ILIb1._1.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ILIb1._1.Controllers
{
    public class UserDashBoardController : Controller
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserDashBoardController(ILoanRepository loanRepository , IHttpContextAccessor httpContextAccessor)
        {
            _loanRepository = loanRepository;
            _httpContextAccessor = httpContextAccessor;

        }



        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                var model = await _loanRepository.GetByUserIdAsync(User.Identity.GetUserId());
                return View(model);

            }
            return RedirectToAction("Index", "Home");
        }





        public async Task<IActionResult> Loan(int Id)
        {
            if (Id == 0) return RedirectToAction("Index");

            if (User.Identity.IsAuthenticated)
            {
                var loan = new Loan()
                {
                    BookId = Id,
                    UserId = User.Identity.GetUserId(),
                    LoanDate = DateTime.Now

                };
                var userloans = _loanRepository.GetByUserIdAsync(User.Identity.GetUserId()).Result;
                foreach (var item in userloans)
                {
                    if (item.BookId == Id) return RedirectToAction("Index", "Book");
                }
                var loanStatus = _loanRepository.AddLoan(loan);
                if (loanStatus)
                {
                    return RedirectToAction("Index", "Book");
                }
                return View("Index", "DashBoard");

            }
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> ReLoan(int Id)
        {
            var updatedLoan = await _loanRepository.GetAll();
            var loan = updatedLoan.Where(p => p.LoanId == Id).FirstOrDefault();

            loan.LoanDate = DateTime.Now;
            var po = _loanRepository.Save();
            if (po)
            {
                return RedirectToAction("Index", "UserDashBoard");
            }
            return RedirectToAction("Index", "Book");
        }


        public async Task<IActionResult> EditProfile()
        {

        }
    }
}
