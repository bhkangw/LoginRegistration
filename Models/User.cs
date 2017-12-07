using System.ComponentModel.DataAnnotations;

namespace LoginRegistration.Models
{
    public class RegisterUser
    {
        [Display(Name = "First Name")]
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
    public class LoginUser
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string LogEmail {get;set;}

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string LogPassword {get;set;}
    }
}