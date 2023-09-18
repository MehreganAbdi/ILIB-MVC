using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILIb1._1.Models
{
	public class Loan
	{
        [Key]
        public int LoanId { get; set; }
        
        [ForeignKey("UserId")][Required]
        public int UserId { get; set; }

        public DateTime? LoanDate { get; set; }
        public ICollection<Book> RentedBooksByUser { get; set; }


    }
}
