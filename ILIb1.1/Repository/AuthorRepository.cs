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
            return await _context.Authors.ToListAsync(); 
        }

        public async Task<IEnumerable<Book>> GetByIdAsync(int Id)
        {
            
            return await _context.Books.Include(b=>b.Author).Where(a => a.AuthorId == Id).ToListAsync();
            
            
        }
        public async Task<IEnumerable<Book>> GetByIdAsyncNoTracking(int Id)
        {

            return await _context.Books.Include(b => b.Author).AsNoTracking().Where(a => a.AuthorId == Id).ToListAsync();


        }

        public async Task<IEnumerable<Author>> GetByName(string name)
        {
            return await _context.Authors.Where(a => a.FullName.Contains(name)).ToListAsync();
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
    }
}
