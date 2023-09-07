using ILIb1._1.InterFaces;
using ILIb1._1.Models;

namespace ILIb1._1.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        bool IAuthorRepository.Add(Author author)
        {
            throw new NotImplementedException();
        }

        bool IAuthorRepository.Delete(Author author)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Author>> IAuthorRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<Author> IAuthorRepository.GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        bool IAuthorRepository.Save()
        {
            throw new NotImplementedException();
        }

        bool IAuthorRepository.Update(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
