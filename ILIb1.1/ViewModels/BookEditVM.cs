using ILIb1._1.Data.Enum;
using ILIb1._1.Models;

namespace ILIb1._1.ViewModels
{
    public class BookEditVM
    {
        public int BookId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int BookCount { get; set; }
        public int? Edition { get; set; }
        public int? Year { get; set; }
        public BookCategory BookCategory { get; set; }
        public Author? Author  { get; set; }
        public int AuthorId { get; set; }
    }
}
