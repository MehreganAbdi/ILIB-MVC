using ILIb1._1.Data;
using ILIb1._1.InterFaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ILIb1._1.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index()
        {
            var Books =await _bookRepository.GetAll();

            return  View(Books);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var Book = await _bookRepository.GetByIdAsync(Id);
            return View(Book);
        }

    }
}
