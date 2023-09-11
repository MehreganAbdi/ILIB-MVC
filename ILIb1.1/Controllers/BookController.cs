using ILIb1._1.Data;
using ILIb1._1.InterFaces;
using ILIb1._1.Models;
using ILIb1._1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ILIb1._1.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookController(IBookRepository bookRepository , IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var Books = await _bookRepository.GetAll();

            return View(Books);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var Book = await _bookRepository.GetByIdAsync(Id);
            return View(Book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            var author = _bookRepository.GetByAuthourName(book.Author.FullName);
            if (author == null)
            {
                _bookRepository.Add(book);
                return RedirectToAction("Index");
            }
            
            var authorId = _authorRepository.GetByIdAsyncNoTracking(author.AuthorId);
            
            author.BooksByAuthor.Add(book);

            var updatedAuthor = new Author
            {
                AuthorId = book.AuthorId,
                FullName = book.Author.FullName,
                BooksByAuthor = author.BooksByAuthor
            };

            _authorRepository.Update(updatedAuthor);

            return RedirectToAction("Index");

        }



        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return RedirectToAction("Index");

            var BookVM = new BookEditVM
            {
                Title = book.Title,
                Year = book.Year,
                Edition = book.Edition,
                Description = book.Description,
                Author = book.Author,
                AuthorId = book.AuthorId,
                BookCategory = book.BookCategory,
                BookCount = book.BookCount

            };
            return View(BookVM);

        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookEditVM bookEditVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var initialId = _bookRepository.GetByIdAsyncAsNoTracking(id);
            if (initialId != null)
            {

                var UpdatedBook = new Book
                {
                    BookId = id,
                    Title = bookEditVM.Title,
                    Year = bookEditVM.Year,
                    Edition = bookEditVM.Edition,
                    Description = bookEditVM.Description,
                    Author = bookEditVM.Author,
                    AuthorId = bookEditVM.AuthorId,
                    BookCategory = bookEditVM.BookCategory,
                    BookCount = bookEditVM.BookCount
                };

                _bookRepository.Update(UpdatedBook);
                return RedirectToAction("Index");
            }
            else
            {
                return View(bookEditVM);
            }
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var theBook = await _bookRepository.GetByIdAsync(Id);
            if (theBook == null) return View("Error");

            return View(theBook);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteBook(int Id)
        {
            var theBook = await _bookRepository.GetByIdAsync(Id);
            if (theBook == null) return View("Error");

            _bookRepository.Delete(theBook);

            return RedirectToAction("Index");
        }

    }
}
