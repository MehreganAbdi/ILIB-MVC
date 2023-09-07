using ILIb1._1.InterFaces;
using ILIb1._1.Models;

namespace ILIb1._1.Repository
{
    public class BookRepository : IBookRepository
    {
        bool IBookRepository.Add(Book book)
        {
            throw new NotImplementedException();
        }

        bool IBookRepository.Delete(Book book)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Book>> IBookRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<Book> IBookRepository.GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        Task<Book> IBookRepository.GetByTitle()
        {
            throw new NotImplementedException();
        }

        bool IBookRepository.Save()
        {
            throw new NotImplementedException();
        }

        bool IBookRepository.Update(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
