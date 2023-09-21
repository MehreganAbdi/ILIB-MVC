using ILIb1._1.Models;
using ILIb1._1.ViewModels;

namespace ILIb1._1.InterFaces
{
    public interface ILoanRepository
    {
        Task<bool> IsBlockedByFines(AppUser User);
        Task<bool> IsBlockByBookCount(AppUser Uesr);
        Task<int> FinesValue(AppUser User);
         Task<int> AddFine(Loan loan);
        bool AddLoan(Loan loan);
        Task<IEnumerable<Loan>> GetAll();
        Task<IEnumerable<Loan>> GetByUserIdAsync(string UserId);
        Task<bool> Recieve(Loan loan);
        bool Save();
        bool UpdateUser(AppUser User);
        Task<AppUser> GetUserAsyncNoTracking(string Id);
        Task<LoanVM> GetLoanDetail(Loan loan);
    }
}
