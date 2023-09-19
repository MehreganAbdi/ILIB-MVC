using ILIb1._1.Data;
using ILIb1._1.InterFaces;
using ILIb1._1.Models;
using Microsoft.EntityFrameworkCore;

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
            foreach (var item in loan.RentedBooksByUser)
            {
                if (item.BookCount < 1) return false;
                
            }
            foreach (var item in loan.RentedBooksByUser)
            {
                item.BookCount--;
                Save();
            }

            _context.Add(loan);
            return Save();
        }

        public bool AddLoan(AppUser User, ICollection<Book> books)
        {

            var loan = new Loan()
            {
                UserId = User.Id,
                LoanDate = DateTime.Now,
                RentedBooksByUser = books,

            };
            if(User.BorrowedBookCount > 6 || User.Fines > 10)
            {
                return false;
            }
            return AddLoan(loan);
            
        }

        public async Task<int> FinesValue(AppUser User)
        {
            var pot =await  _context.Users.Where(p => p == User).FirstOrDefaultAsync();
            
            
            return  (int)pot.Fines;
        }

        public Task<int> FinesValue(Loan loan)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Loan>> GetAll()
        {
            return await _context.Loans.ToListAsync();
        }

        //  ?
        public async Task<IEnumerable<Loan>> GetByUserIdAsync(string UserId)
        {
            return await _context.Loans.Include(l => l.UserId).Where(p => p.UserId == UserId).ToListAsync();
        }

        public async Task<bool> IsBlockByBookCount(AppUser User)
        {
            var x = await _context.Users.Where(p => p == User).FirstOrDefaultAsync();
            return  x.BorrowedBookCount > 6 ? true : false;
        }

        public async Task<bool> IsBlockedByFines(AppUser User)
        {
            var x = await _context.Users.Where(p => p == User).FirstOrDefaultAsync();
            return x.Fines > 10 ? true : false;
        }

        public async Task<bool> Recieve(Loan loan, AppUser User)
        {
            foreach (var item in loan.RentedBooksByUser)
            {
                var y = await _context.Books.Where(b => b == item).FirstOrDefaultAsync();
                y.BookCount++;
                Save();
            }

            var fine = (DateTime.Now - (loan.LoanDate)).Value.Days;
            if (fine > 13)
            {
                User.Fines += (14 - fine);
                Save();
            }
            foreach (var item in loan.RentedBooksByUser)
            {
                User.UserBooks.Remove(item);
                Save();
            }
            return Save();
       }
        public bool Save()
        {
            var saveVal = _context.SaveChanges();
            return saveVal > 0 ? true : false;
        }
    }
}
