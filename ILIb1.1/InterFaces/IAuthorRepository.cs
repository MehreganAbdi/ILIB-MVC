using ILIb1._1.Models;

namespace ILIb1._1.InterFaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAll();
        Task<Author> GetByIdAsync(int Id);
        Task<Author> GetByIdAsyncNoTracking(int Id);
        Task<IEnumerable<Book>> GetBooksByAuthor(int Id);
        bool Add(Author author);
        bool Delete(Author author);
        bool Update(Author author);
        bool Save();
    }
}
