using System.ComponentModel.DataAnnotations;

namespace ILIb1._1.Models
{
	public class Author
	{
        [Key]
        public int AuthorId { get; set; }
        public string FullName { get; set; }
        
    }
}
