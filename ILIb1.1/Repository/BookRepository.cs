using ILIb1._1.Data;
using ILIb1._1.InterFaces;
using ILIb1._1.Models;
using Microsoft.EntityFrameworkCore;

namespace ILIb1._1.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDBContext _context;

        public BookRepository(ApplicationDBContext context)
        {
                _context = context;
        }

        public bool Add(Book book)
        {
            _context.Add(book);

            return Save();
        }

        public bool Delete(Book book)
        {
            _context.Remove(book);

            return Save();
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int Id)
        {
            return await _context.Books.Include(a=>a.Author).FirstOrDefaultAsync(b => b.BookId == Id) ;
        }
        public async Task<Book> GetByIdAsyncAsNoTracking(int Id)
        {
            return await _context.Books.Include(a => a.Author).AsNoTracking().FirstOrDefaultAsync(b => b.BookId == Id);
        }

        public async Task<IEnumerable<Book>> GetByTitle(string title)
        {
            return await _context.Books.Where(b => b.Title.Contains(title)).ToListAsync();
        }


        public bool Update(Book book)
        {
            _context.Update(book);
            return Save();
        }

        public bool Save()
        {
            var saveVal = _context.SaveChanges();
            return saveVal > 0 ? true : false;
        }

        
    }
}
