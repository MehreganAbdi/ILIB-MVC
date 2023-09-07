using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace ILIb1._1.Models
{
	//Library Members
	public class AppUser : IdentityUser
	{
        [Key]
        public int AppUserId { get; set; }

        public int? Fines { get; set; }

		public int? BorrowedBookCount { get; set; }
        public ICollection<Book> UserBooks { get; set; }


    }
}
