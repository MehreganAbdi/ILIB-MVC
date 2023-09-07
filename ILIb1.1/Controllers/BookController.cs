using ILIb1._1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ILIb1._1.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _context;

        public BookController( ApplicationDBContext context )
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Books = _context.Books.ToList();
            return View(Books);
        }

        public IActionResult Detail(int Id)
        {
            var Book = _context.Books.Include(a => a.Author).FirstOrDefault(p => p.BookId == Id);

            return View(Book);
        }

    }
}
