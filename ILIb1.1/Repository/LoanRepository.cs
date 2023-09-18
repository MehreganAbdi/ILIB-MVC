using ILIb1._1.Data;
using ILIb1._1.InterFaces;
using ILIb1._1.Models;

namespace ILIb1._1.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDBContext _context;
        public LoanRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public bool AddLoan(Loan loan)
        {
            _context.Add(loan);
            return Save();
        }

        public bool AddLoan(AppUser User, ICollection<Book> books)
        {

            var loan = new Loan() {
            UserId = User.Id,
            };
        }

        public Task<int> FinesValue(AppUser User)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Loan>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Loan>> GetByUserIdAsync(string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsBlockByBookCount(AppUser Uesr)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsBlockedByFines(AppUser User)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Recieve(int LoanId, AppUser User)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Recieve(int LoanId, string UserId)
        {
            throw new NotImplementedException();
        }
        public bool Save()
        {
            var saveVal = _context.SaveChanges();
            return saveVal > 0 ? true : false;
        }
    }
}
