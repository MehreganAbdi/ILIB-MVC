using ILIb1._1.Models;

namespace ILIb1._1.InterFaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAll();

        Task<Author> GetByIdAsync(int Id);
        bool Add(Author author);
        bool Delete(Author author);
        bool Update(Author author);
        bool Save();
    }
}
