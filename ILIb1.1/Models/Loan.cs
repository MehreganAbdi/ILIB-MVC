using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILIb1._1.Models
{
	public class Loan
	{
        [Key]
        public int LoanId { get; set; }
        [ForeignKey("Book")][Required]
        public int BookId { get; set; }
        [ForeignKey("AppUser")][Required]
        public int AppUserId { get; set; }

    }
}
