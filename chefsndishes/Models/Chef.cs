using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using chefsndishes.Class;

namespace chefsndishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId {get; set;}
        [Display(Name ="First Name")] 
        [Required(ErrorMessage = "You must enter a First name")]
        public string FirstName {get; set;}
        [Required(ErrorMessage = "You must enter a Last name")]
        [Display(Name ="Last Name")] 
        public string LastName {get; set;}
        [Display(Name ="Birthday")] 
        [Required(ErrorMessage = "You must select the chefs birthday")]
        [FutureDate(ErrorMessage ="Date must be in the past")]
        public DateTime Birthday {get; set;}
        public List<Dish> CreatedDishes {get; set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}