using ILIb1._1.InterFaces;
using ILIb1._1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
                return  View(model);

            }
            return RedirectToAction("Index", "Home");
        }


    }
}
