using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}
        [Required(ErrorMessage = "You must enter a first name")]
        [MinLength(2, ErrorMessage ="You need at least 4 characters in your first name")]
        [Display(Name = "First Name: ")]
        public string FirstName {get; set;}

        [Required(ErrorMessage = "You must enter a last name")]
        [MinLength(2, ErrorMessage ="You need at least 4 characters in your last name")]
        [Display(Name = "Last Name: ")]
        public string LastName {get; set;}

        [Required(ErrorMessage = "You must enter your email address")]
        [RegularExpression(@"^[^@]+@[^@]+\.[^@]+$", ErrorMessage ="Must be a valid email address")]
        [Display(Name = "Email Adddress: ")]
        public string Email {get; set;}

        [Required(ErrorMessage = "You must enter a password")]
        [MinLength(8, ErrorMessage ="You need an 8 character password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string Password {get; set;}

        [NotMapped]
        [Required(ErrorMessage = "You confirm your password")]
        [MinLength(8, ErrorMessage ="You need an 8 character password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password: ")]
        [Compare("Password", ErrorMessage ="Passwords do not match")]
        public string ConfirmPassword {get; set;}
        public List<Guests> Attending {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}