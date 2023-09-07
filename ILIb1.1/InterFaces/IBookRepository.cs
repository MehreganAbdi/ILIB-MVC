using ILIb1._1.Models;

namespace ILIb1._1.InterFaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetByTitle();
        Task<Book> GetByIdAsync(int Id);
        bool Add(Book book);
        bool Delete(Book book);
        bool Update(Book book);
        bool Save();
    }
}
