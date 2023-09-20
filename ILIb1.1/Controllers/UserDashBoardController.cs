using ILIb1._1.InterFaces;
using ILIb1._1.Models;
using ILIb1._1.Repository;
using ILIb1._1.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ILIb1._1.Controllers
{
    public class UserDashBoardController : Controller
    {
        private readonly ILoanRepository _loanRepository;
        
        public UserDashBoardController(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;

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
            var UserId = User.Identity.GetUserId();
            var user = await _loanRepository.GetUserAsyncNoTracking(UserId);
            var VModel = new EditProfVM()
            {
                UserName = user.UserName==null?"null":user.UserName,
                PhoneNumber = user.PhoneNumber==null?0:user.PhoneNumber
            };
            return View(VModel); 

        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfVM editProfVM)
        {
            var UserId = User.Identity.GetUserId();
            var user = await _loanRepository.GetUserAsyncNoTracking(UserId);

            user.PhoneNumber = editProfVM.PhoneNumber;
            user.UserName = editProfVM.UserName;
            if (_loanRepository.UpdateUser(user))
            {
                return View("Index", "UserDashBoard");
            }
            return View(editProfVM);
        }
    }
}
