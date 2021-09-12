using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BankAccounts.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^@]+@[^@]+\.[^@]+$", ErrorMessage ="Must be a valid email address")]
        [Display(Name="Email: ")]
        public string LoginEmail {get;set;}

        [Required(ErrorMessage = "You must enter your password")]
        [MinLength(8, ErrorMessage ="You need an 8 character password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string LoginPassword {get; set;}
    }
}