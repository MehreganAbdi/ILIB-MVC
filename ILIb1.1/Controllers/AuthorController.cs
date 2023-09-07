using ILIb1._1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ILIb1._1.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDBContext _context;
        public AuthorController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Authors = _context.Authors.ToList();
            return View(Authors);
        }
        
        public IActionResult Detail(int id)
        {
            var dataList = _context.Books.Include(a => a.Author).Where(p => p.AuthorId == id).ToList();
            return View(dataList); 
        }
    
    }
}
