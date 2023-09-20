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

            _context.Add(loan);
            var response = Save();
            if (response)
            {
                var x = _context.Users.FirstOrDefault(p => p.Id == loan.UserId);
                if (x.BorrowedBookCount == null)
                {
                    x.BorrowedBookCount = 1;
                    return Save();
                }
                else
                {
                    x.BorrowedBookCount += 1;
                    return Save();
                }

            }
            return false;
        }



        public async Task<int> FinesValue(AppUser User)
        {
            var pot = await _context.Users.Where(p => p == User).FirstOrDefaultAsync();


            return (int)pot.Fines;
        }

        public async Task<int> AddFine(Loan loan)
        {
            var x = await _context.Users.Where(p => p.Id == loan.UserId).FirstOrDefaultAsync();
            var lateDays = (DateTime.Now - loan.LoanDate).Value.Days;
            if (lateDays > 14)
            {
                x.Fines += (lateDays - 14);
                Save();
            }
            return (int)x.Fines;
        }

        public async Task<IEnumerable<Loan>> GetAll()
        {
            return await _context.Loans.ToListAsync();
        }

        //  ?
        public async Task<IEnumerable<Loan>> GetByUserIdAsync(string UserId)
        {

            return await _context.Loans.Where(p => p.UserId == UserId).ToListAsync();
        }

        public async Task<bool> IsBlockByBookCount(AppUser User)
        {
            var x = await _context.Users.Where(p => p == User).FirstOrDefaultAsync();
            return x.BorrowedBookCount > 6 ? true : false;
        }

        public async Task<bool> IsBlockedByFines(AppUser User)
        {
            var x = await _context.Users.Where(p => p == User).FirstOrDefaultAsync();
            return x.Fines > 10 ? true : false;
        }

        public async Task<bool> Recieve(Loan loan)
        {
            var x = await AddFine(loan);
            var user = await _context.Users.Where(p => p.Id == loan.UserId).FirstOrDefaultAsync();

            if (user == null)
            {
                return false;
            }
            user.BorrowedBookCount--;

            user.Fines += x;

            return Save();
        }
        public async Task<AppUser> GetUserAsyncNoTracking(string Id)
        {
            return await _context.Users.Where(p => p.Id == Id).AsNoTracking().FirstOrDefaultAsync();
        }
        public bool UpdateUser(AppUser User)
        {
            var saved = _context.Users.Update(User);
            return Save();
        
        }

        public bool Save()
        {
            var saveVal = _context.SaveChanges();
            return saveVal > 0 ? true : false;
        }
    }
}
