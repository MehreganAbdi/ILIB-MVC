using ILIb1._1.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILIb1._1.Models
{
	public class Book
	{
        [Key]
        public int BookId { get; set; }
        
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int BookCount { get; set; }
        public int? Edition { get; set; }
        public int? Year { get; set; }

        public string? Image { get; set; }
        public BookCategory BookCategory { get; set; }
        
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
         

    }
}
