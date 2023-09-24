using ILIb1._1.Data;
using ILIb1._1.InterFaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ILIb1._1.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<IActionResult> Index(string searching)
        {
            var Authors = await _authorRepository.GetAll();
            if (searching == null)
            {
                return View(Authors.DistinctBy(p => p.FullName));
            }
            Authors = Authors.Where(a => a.FullName.Contains(searching)).DistinctBy(p => p.FullName).ToList();
            return View(Authors);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var dataList = await _authorRepository.GetBooksByAuthor(Id);
            return View(dataList);
        }


        public async Task<IActionResult> Delete(int Id)
        {
            var author = await _authorRepository.GetByIdAsync(Id);
            if (author == null) return View("Error");


            return View(author);
        }



    }
}
