using ILIb1._1.Models;

namespace ILIb1._1.InterFaces
{
    public interface ILoanRepository
    {
        Task<bool> IsBlockedByFines(AppUser User);
        Task<bool> IsBlockByBookCount(AppUser Uesr);
        Task<int> FinesValue(AppUser User);
        bool AddLoan(Loan loan);
        bool AddLoan(AppUser User, ICollection<Book> books);
        Task<IEnumerable<Loan>> GetAll();
        Task<IEnumerable<Loan>> GetByUserIdAsync(string UserId);
        Task<bool> Recieve(int LoanId, AppUser User);
        Task<bool> Recieve(int LoanId, string UserId);
        bool Save();

    }
}
