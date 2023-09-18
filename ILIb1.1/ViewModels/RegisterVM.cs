using System.ComponentModel.DataAnnotations;

namespace ILIb1._1.ViewModels
{
    public class RegisterVM
    {
        [Display(Name ="Email Address")]
        [Required(ErrorMessage ="Email Address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Confirm Password")]
        [Required(ErrorMessage ="Confirm The Password!")]
        [DataType(DataType.Password)]
        [Compare("Password" , ErrorMessage ="Passwords Are Different")]
        public string ConfirmPassword { get; set; }
    }
}
