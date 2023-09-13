using ILIb1._1.Data;
using ILIb1._1.InterFaces;
using ILIb1._1.Models;
using Microsoft.EntityFrameworkCore;

namespace ILIb1._1.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDBContext _context;

        public AuthorRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public bool Add(Author author)
        {
            _context.Add(author);

            return Save();
        }

        public bool Delete(Author author)
        {
            _context.Remove(author);

            return Save();
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _context.Books.Include(p => p.Author).Select(o => o.Author).ToListAsync(); 
        }

        public async Task<Author> GetByIdAsync(int Id)
        {
            
            return await _context.Authors.Where(p => p.AuthorId == Id).FirstOrDefaultAsync();


        }

        public async Task<Author> GetByIdAsyncNoTracking(int Id)
        {

            return await _context.Authors.Where(p => p.AuthorId == Id).AsNoTracking().FirstOrDefaultAsync(); 


        }

        

        public bool Save()
        {
            var saveVal = _context.SaveChanges();
            return saveVal > 0 ? true:false;
        }

        public bool Update(Author author)
        {
            _context.Update(author);
            return Save();
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthor(int Id)
        {
            var authorName =  GetByIdAsync(Id).Result.FullName;
            return await _context.Books.Include(a => a.Author).Where(b => b.Author.FullName == authorName).ToListAsync();
        }
    }
}
