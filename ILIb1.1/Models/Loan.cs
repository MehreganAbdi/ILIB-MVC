using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILIb1._1.Models
{
	public class Loan
	{
        [Key]
        public int LoanId { get; set; }
        
        [ForeignKey("UserId")]
        [Required]
        public string UserId { get; set; }
        
        
        [ForeignKey("BookId")]
        [Required]
        public int BookId { get; set; }

        public DateTime? LoanDate { get; set; }
       


    }
}
